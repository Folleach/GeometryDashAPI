using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Server;

[assembly: InternalsVisibleTo("GeometryDashAPI.Tests")]
[assembly: InternalsVisibleTo("GeometryDashAPI.Benchmarks")]

namespace GeometryDashAPI
{
    public class GeometryDashApi
    {
        internal static IGameParser parser = new ObjectParser();

        private static readonly Dictionary<Type, Func<string, object>> StringToObjectParsers = new ()
            {
                { typeof(bool), x => GameConvert.StringToBool(x) },
                { typeof(byte), x => byte.Parse(x) },
                { typeof(short), x => short.Parse(x) },
                { typeof(int), x => int.Parse(x) },
                { typeof(int?), x => string.IsNullOrEmpty(x) ? (int?)null : int.Parse(x) },
                { typeof(long), x => long.Parse(x) },
                { typeof(double), x => GameConvert.StringToDouble(x) },
                { typeof(float), x => GameConvert.StringToSingle(x) },
                { typeof(string), x => x },
                { typeof(BlockGroup), BlockGroup.Parse },
                { typeof(Hsv), Hsv.Parse },
                { typeof(Pagination), Pagination.Parse}
            };
        private static readonly Dictionary<Type, Func<object, string>> ObjectToStringParsers = new ()
            {
                { typeof(bool), x => GameConvert.BoolToString((bool)x) },
                { typeof(byte), x => x.ToString() },
                { typeof(short), x => x.ToString() },
                { typeof(int), x => x.ToString() },
                { typeof(int?), x => x?.ToString() ?? string.Empty },
                { typeof(long), x => x.ToString() },
                { typeof(double), x => GameConvert.DoubleToString((double)x) },
                { typeof(float), x => GameConvert.SingleToString((float)x) },
                { typeof(string), x => (string)x },
                { typeof(BlockGroup), x => ((BlockGroup)x).ToString() },
                { typeof(Hsv), x => Hsv.Parse((Hsv)x) },
                { typeof(Pagination), x => Pagination.Parse((Pagination)x)}
            };

        private static readonly Dictionary<Type, TypeDescription<string, GamePropertyAttribute>> ObjectCache = new();
        private static readonly Dictionary<Type, TypeDescription<int, AsStructAttribute>> StructCache = new();
        private static readonly Dictionary<Type, Td> tds = new();

        private static readonly Dictionary<int, Type> BlockTypes = new();

        static GeometryDashApi()
        {
            RegisterBlockTypes(typeof(GeometryDashApi).Assembly, false);
        }

        internal static TypeDescription<string, GamePropertyAttribute> GetGamePropertyCache(Type type)
        {
            if (ObjectCache.TryGetValue(type, out var description))
                return description;
            return ObjectCache[type] = new TypeDescription<string, GamePropertyAttribute>(type, attribute => attribute.Key);
        }

        // internal static TypeDescription<int, AsStructAttribute> GetStructMemberCache(Type type)
        // {
        //     if (StructCache.TryGetValue(type, out var description))
        //         return description;
        //     return StructCache[type] = new TypeDescription<int, AsStructAttribute>(type, attribute => attribute.Position);
        // }

        public static void RegisterBlockTypes(Assembly assembly, bool overrideTypes)
        {
            foreach (var type in assembly.GetTypes())
                RegisterBlockType(type, overrideTypes);
        }

        public static void RegisterBlockType(Type type, bool overrideType)
        {
            foreach (var item in type.GetCustomAttributes(false))
            {
                if (item is not GameBlockAttribute attribute)
                    continue;
                foreach (var id in attribute.Ids)
                {
                    if (overrideType)
                        BlockTypes[id] = type;
                    else
                        BlockTypes.Add(id, type);
                }
            }
        }
        
        public static Func<string, object> GetStringParser(Type type)
        {
            if (StringToObjectParsers.TryGetValue(type, out var parser))
                return parser;
            Td td = null;
            if (!tds.TryGetValue(type, out td))
                td = GetTypeDescription(type);
            if (td!.IsGameObject)
                return raw => GeometryDashApi.parser.Decode(type, raw);
            if (td!.IsGameStruct) ;
                // return raw => StructParser.Decode(type, raw);
            if (type.IsEnum)
                return raw => Enum.Parse(type, raw);
            // if (td!.IsArray)
            //     return raw => ArrayParser.Decode(type, arraySeperators[type], raw);
            throw new Exception($"Couldn't parse: {type}");
        }

        public static Func<object, string> GetObjectParser(Type type)
        {
            if (ObjectToStringParsers.TryGetValue(type, out var parser))
                return parser;
            Td td = null;
            if (!tds.TryGetValue(type, out td))
                td = GetTypeDescription(type);
            if (td.IsGameObject)
                 return value => GeometryDashApi.parser.Encode(type, (GameObject)value);
            if (td.IsGameStruct) ;
                 // return value => StructParser.Encode(type, (GameStruct)value);
            throw new Exception($"Couldn't parse: {type}");
        }
        
        public static Type GetBlockType(int blockId)
        {
            if (BlockTypes.TryGetValue(blockId, out var type))
                return type;
            return typeof(Block);
        }

        private static Td GetTypeDescription(Type type)
        {
            if (!tds.TryGetValue(type, out var description))
                tds.Add(type, description = new Td(type));
            return description;
        }
    }
}