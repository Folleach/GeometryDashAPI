using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Parsers
{
    public class ObjectParser : IGameParser
    {
        private static Dictionary<Type, IDescriptor<IGameObject, int>> descriptors = new();

        public T Decode<T>(string raw) where T : GameObject, new()
        {
            var instance = new T();
            return (T) Decode(typeof(T), raw, instance.GetParserSense());
        }

        public T Decode<T>(ReadOnlySpan<char> raw) where T : GameObject, new()
        {
            return Decode<T>(raw.ToString());
        }

        public string Encode<T>(T obj) where T : GameObject
        {
            return Encode(typeof(T), obj);
        }

        public List<T> DecodeArray<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject
        {
            var parser = new LLParserSpan(separator, raw);
            Span<char> value;
            var descriptor = descriptors.GetOrCreate(typeof(T), CreateDescriptor);
            var list = new List<T>();
            while ((value = parser.Next()) != null)
                list.Add((T)descriptor.Create(value));
            return list;
        }

        public IGameObject DecodeBlock(string raw)
        {
            return DecodeBlock(raw.AsSpan());
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

            return Decode(type, raw, ",");
        }

        public string EncodeBlock(Block block)
        {
            return Encode(block.GetType(), block);
        }

        public IGameObject Decode(Type type, string raw)
        {
            var instance = (GameObject) Activator.CreateInstance(type);
            return Decode(type, raw, instance.GetParserSense());
        }

        private static IGameObject Decode(Type type, ReadOnlySpan<char> raw, ReadOnlySpan<char> sense)
        {
            var descriptor = descriptors.GetOrCreate(type, CreateDescriptor);
            return descriptor.Create(raw);
        }

        public string Encode(Type type, GameObject obj)
        {
            var builder = new StringBuilder();
            var description = GeometryDashApi.GetGamePropertyCache(type);
            var needSeparate = false;
            var parserSense = obj.GetParserSense();

            void AppendKey(string key)
            {
                if (needSeparate)
                    builder.Append(parserSense);
                needSeparate = true;
                builder.Append(key);
                builder.Append(parserSense);
            }

            foreach (var member in description.Members.Values)
            {
                var value = member.GetValue(obj);
                if (!member.Attribute.AlwaysSet && Equals(member.Attribute.DefaultValue, value))
                    continue;

                AppendKey(member.Attribute.Key);
                var parser = GeometryDashApi.GetObjectParser(member.MemberType);
                builder.Append(parser(value));
            }

            foreach (var item in obj.WithoutLoaded)
            {
                if (needSeparate)
                    builder.Append(parserSense);
                builder.Append(item.Key);
                builder.Append(parserSense);
                builder.Append(item.Value);
            }

            return builder.ToString();
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