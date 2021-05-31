using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Parser;
using GeometryDashAPI.Parsers;

[assembly: InternalsVisibleTo("GeometryDashAPI.Tests")]

namespace GeometryDashAPI
{
    public class GeometryDashApi
    {
        internal static readonly Dictionary<Type, Func<string, object>> StringToObjectParsers
            = new Dictionary<Type, Func<string, object>>()
            {
                { typeof(bool), x => GameConvert.StringToBool(x) },
                { typeof(byte), x => byte.Parse(x) },
                { typeof(short), x => short.Parse(x) },
                { typeof(int), x => int.Parse(x) },
                { typeof(long), x => long.Parse(x) },
                { typeof(double), x => GameConvert.StringToDouble(x) },
                { typeof(float), x => GameConvert.StringToSingle(x) },
                { typeof(string), x => x }
            };
        internal static readonly Dictionary<Type, Func<object, string>> ObjectToStringParsers
            = new Dictionary<Type, Func<object, string>>()
            {
                { typeof(bool), x => GameConvert.BoolToString((bool)x) },
                { typeof(byte), x => x.ToString() },
                { typeof(short), x => x.ToString() },
                { typeof(int), x => x.ToString() },
                { typeof(long), x => x.ToString() },
                { typeof(double), x => GameConvert.DoubleToString((double)x) },
                { typeof(float), x => GameConvert.SingleToString((float)x) },
                { typeof(string), x => (string)x }
            };
        private static readonly Dictionary<Type, GameTypeDescription> TypesDescriptionsCache
            = new Dictionary<Type, GameTypeDescription>();

        private static readonly Dictionary<int, Type> BlockTypes = new Dictionary<int, Type>();

        static GeometryDashApi()
        {
            foreach (var type in typeof(GeometryDashApi).Assembly.GetTypes())
            {
                foreach (var item in type.GetCustomAttributes(false))
                {
                    if (!(item is GameBlockAttribute attribute))
                        continue;
                    foreach (var id in attribute.Ids)
                        BlockTypes.Add(id, type);
                }
            }
        }

        internal static GameTypeDescription GetDescription(Type type)
        {
            if (TypesDescriptionsCache.TryGetValue(type, out var description))
                return description;
            return TypesDescriptionsCache[type] = new GameTypeDescription(type);
        }

        public static Type GetBlockType(int blockId)
        {
            if (BlockTypes.TryGetValue(blockId, out var type))
                return type;
            throw new Exception($"Can't find type for blockId: {blockId}");
        }
    }
}