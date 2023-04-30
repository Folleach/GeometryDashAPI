using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GeometryDashAPI.Serialization
{
    public class ObjectSerializer : IGameSerializer
    {
        private static Dictionary<Type, IDescriptor<IGameObject>> descriptors = new();

        private static ConcurrentDictionary<Type, Action<IGameObject, StringBuilder>> copiersCache = new();

        public T Decode<T>(ReadOnlySpan<char> raw) where T : IGameObject
        {
            return (T)Decode(typeof(T), raw);
        }

        public ReadOnlySpan<char> Encode<T>(T value) where T : IGameObject
        {
            var descriptor = descriptors.GetOrCreate(typeof(T), CreateDescriptor);
            if (descriptor is not ICopyDescriptor<T> copyDescriptor)
                throw new InvalidOperationException($"descriptor is not implement '{typeof(ICopyDescriptor<T>)}'");
            var builder = new StringBuilder();
            copyDescriptor.CopyTo(value, builder);
            return builder.ToString();
        }

        public Action<IGameObject, StringBuilder> GetCopier(Type type) => copiersCache.GetOrAdd(type, CreateCopier);

        private static Action<IGameObject, StringBuilder> CreateCopier(Type type)
        {
            Expression descriptor = Expression.Constant(descriptors.GetOrCreate(type, CreateDescriptor));
            var instance = Expression.Parameter(typeof(IGameObject), "instance");
            var destination = Expression.Parameter(typeof(StringBuilder), "destination");
            descriptor = Expression.Convert(descriptor, typeof(ICopyDescriptor<>).MakeGenericType(type));

            var method = typeof(ICopyDescriptor<>).MakeGenericType(type).GetMethod("CopyTo");
            if (method == null)
                throw new InvalidOperationException($"method 'CopyTo' not found in descriptor for type: {type}");

            var call = Expression.Call(descriptor, method, Expression.Convert(instance, type), destination);
            return (Action<IGameObject, StringBuilder>)Expression.Lambda(call, instance, destination).Compile();
        }

        public List<T> DecodeList<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject
        {
            var parser = new LLParserSpan(separator, raw);
            ReadOnlySpan<char> value;
            var list = new List<T>();
            while ((value = parser.Next()) != null)
                list.Add((T)Decode(typeof(T), value));
            return list;
        }

        public T[] GetArray<T>(ReadOnlySpan<char> raw, string separator, IGameSerializer.Parser<T> getValue)
        {
            var parser = new LLParserSpan(separator, raw);
            var length = parser.GetCountOfValues();
            ReadOnlySpan<char> rawValue;
            var array = new T[length];
            var index = 0;
            while ((rawValue = parser.Next()) != null)
                array[index++] = getValue(rawValue);
            return array;
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

        /// <summary>
        /// Uses in <see cref="TypeDescriptor{T}"/>
        /// </summary>
        internal IDescriptor<T> GetDescriptor<T>() where T : IGameObject
            => (IDescriptor<T>)descriptors.GetOrCreate(typeof(T), CreateDescriptor);

        private static IGameObject Decode(Type type, ReadOnlySpan<char> raw)
        {
            var descriptor = descriptors.GetOrCreate(type, CreateDescriptor);
            return descriptor.Create(raw);
        }

        private static IDescriptor<IGameObject> CreateDescriptor(Type type)
        {
            var descriptorType = typeof(TypeDescriptor<>);
            var generic = descriptorType.MakeGenericType(type);
            var instance = (IDescriptor<IGameObject>)Activator.CreateInstance(generic);
            return instance;
        }
    }
}
