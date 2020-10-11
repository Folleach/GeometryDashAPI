using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace GeometryDashAPI.Parsers
{
    public class LevelParser
    {
        private static readonly SLLParser semicolonSll = new SLLParser(';');
        private static readonly SLLParser straightSll = new SLLParser('|');
        private static readonly Type[] emptryTypes = new Type[0];
        private static readonly object[] emptyArgs = new object[0];

        private Dictionary<Type, GameObjectAttribute> objectsInfo = new Dictionary<Type, GameObjectAttribute>();
        private PropertyMapping blocksMap = new PropertyMapping();
        private PropertyMapping settingsMap = new PropertyMapping();
        private Dictionary<string, ConstructorInfo> constructorsInfo = new Dictionary<string, ConstructorInfo>();
        private Dictionary<Type, Func<string, object>> stringParsers = new Dictionary<Type, Func<string, object>>()
        {
            { typeof(bool), (data) => GameConvert.StringToBool(data) },
            { typeof(byte), (data) => byte.Parse(data) },
            { typeof(short), (data) => short.Parse(data) },
            { typeof(int), (data) => int.Parse(data) },
            { typeof(float), (data) => GameConvert.StringToSingle(data) },
            { typeof(string), (data) => data }
        };

        public LevelParser(TypeMapping mapping)
        {
            InitialType(blocksMap, typeof(Block));
            InitialType(settingsMap, typeof(Level));
            foreach (var item in mapping.Types)
            {
                var attribute = item.GetCustomAttribute<GameObjectAttribute>();
                if (attribute == null)
                    throw new Exception($"Object '{item.Name}' doesn't contains GameObjectAttribute");
                objectsInfo.Add(item, attribute);
                InitialType(blocksMap, item);
            }
            // TODO: May be optimize
            foreach (var item in mapping)
            {
                var constructor = item.Value.GetConstructor(emptryTypes);
                if (constructor == null)
                    throw new Exception($"Object '{item.Value.Name}' doesn't contains empty constructor");
                constructorsInfo.Add(item.Key, constructor);
            }
        }

        public Level Parse(string data, Level level = null)
        {
#if DEBUG
            var sw = Stopwatch.StartNew();
#endif
            data = Crypt.GZipDecompress(GameConvert.FromBase64(data));
            level = level ?? new Level();
            IEnumerator<string> mainFlow = semicolonSll.Parse(data).GetEnumerator();
            bool readed = mainFlow.MoveNext();
            if (!readed)
                return null;
            KeyValueSLLParser flow = new KeyValueSLLParser(',', mainFlow.Current);
            LoadHead(level, flow);
            while (mainFlow.MoveNext())
            {
                flow.SetValue(mainFlow.Current);
                level.AddBlock(ReadBlock(flow));
            }
#if DEBUG
            Debug.WriteLine($"[TIME] Parse level: {sw.ElapsedMilliseconds} ms");
#endif
            return level;
        }

        private void LoadHead(Level level, KeyValueSLLParser flow)
        {
            while (flow.Next())
            {
                if (flow.Key == "kS38")
                {
                    LoadColors(level, flow.Value);
                    continue;
                }
                PropertyInfo property = settingsMap.GetProperty(flow.Key);
                property.SetValue(level, Parse(property.PropertyType, flow.Value));
            }
        }

        private void LoadColors(Level level, string data)
        {
            foreach (var item in straightSll.Parse(data))
                level.AddColor(new Color(item));
        }

        private IBlock ReadBlock(KeyValueSLLParser kvsll)
        {
            kvsll.Next();
            // TODO: Need fix
            if (kvsll.Key != "1")
                throw new Exception("Incorrect block format. Maybe correct but Folleach is lazy");
            IBlock instance = (IBlock)constructorsInfo[kvsll.Value].Invoke(emptyArgs);
            instance.ID = int.Parse(kvsll.Value);
            while (kvsll.Next())
            {
                PropertyInfo property = blocksMap.GetProperty(kvsll.Key);
                property.SetValue(instance, Parse(property.PropertyType, kvsll.Value));
            }
            return instance;
        }

        private object Parse(Type type, string value)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value);
            return stringParsers[type](value);
        }

        private void InitialType(PropertyMapping map, Type type)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var attribute = property.GetCustomAttribute<GamePropertyAttribute>();
                if (attribute == null)
                    continue;
                map.Register(property, attribute);
            }
        }
    }
}
