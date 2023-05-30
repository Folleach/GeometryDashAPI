using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Serialization;

[assembly: InternalsVisibleTo("GeometryDashAPI.Tests")]
[assembly: InternalsVisibleTo("GeometryDashAPI.Benchmarks")]
[assembly: InternalsVisibleTo("Examples")]

namespace GeometryDashAPI
{
    public class GeometryDashApi
    {
        public static readonly int BinaryVersion = 35;

        internal static ObjectSerializer Serializer = new();

        private static readonly Dictionary<int, Type> BlockTypes = new();

        static GeometryDashApi()
        {
            RegisterBlockTypes(typeof(GeometryDashApi).Assembly, false);
        }

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

        public static Type GetBlockType(int blockId)
        {
            return BlockTypes.TryGetValue(blockId, out var type) ? type : typeof(Block);
        }
    }
}