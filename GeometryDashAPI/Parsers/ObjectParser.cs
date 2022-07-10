using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Parsers
{
    public class ObjectParser : IGameParser
    {
        private static Dictionary<Type, IDescriptor<IGameObject, int>> descriptors = new();

        public T Decode<T>(ReadOnlySpan<char> raw) where T : IGameObject
        {
            return (T)Decode(typeof(T), raw);
        }

        public List<T> DecodeArray<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject
        {
            var parser = new LLParserSpan(separator, raw);
            Span<char> value;
            var list = new List<T>();
            while ((value = parser.Next()) != null)
                list.Add((T)Decode(typeof(T), value));
            return list;
        }

        public IGameObject DecodeBlock(ReadOnlySpan<char> raw)
        {
            var parser = new LLParserSpan(",", raw);
            ReadOnlySpan<char> key;
            ReadOnlySpan<char> blockId;
            while (parser.TryParseNext(out key, out blockId))
            {
                if (key.SequenceEqual("1"))
                    goto FoundKey;
            }
            throw new Exception("Raw data doesn't contains id component");
            FoundKey:
            if (!int.TryParse(blockId, out var id))
                throw new Exception("Id is not a number");
            var type = GeometryDashApi.GetBlockType(id);

            return Decode(type, raw);
        }

        private static IGameObject Decode(Type type, ReadOnlySpan<char> raw)
        {
            var descriptor = descriptors.GetOrCreate(type, CreateDescriptor);
            return descriptor.Create(raw);
        }
        
        private static IDescriptor<IGameObject, int> CreateDescriptor(Type type)
        {
            var descriptorType = typeof(TypeDescriptor<,>);
            var generic = descriptorType.MakeGenericType(type, typeof(int));
            var instance = (IDescriptor<IGameObject, int>)Activator.CreateInstance(generic);
            return instance;
        }
    }
}