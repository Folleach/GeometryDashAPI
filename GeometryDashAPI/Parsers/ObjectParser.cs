using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Parsers
{
    public class ObjectParser : IGameParser
    {
        private static Dictionary<Type, IDescriptor<IGameObject, int>> descriptors = new();
        
        private readonly Dictionary<string, string> v1 = new();
        private readonly Dictionary<string, string> v2 = new();
        private readonly Dictionary<string, string> v3 = new();

        public T Decode<T>(string raw) where T : GameObject, new()
        {
            var instance = new T();
            v1.Clear();
            return (T) Decode(typeof(T), Parse(raw, instance.GetParserSense(), v1), instance);
        }

        public T Decode<T>(ReadOnlySpan<char> raw) where T : GameObject, new()
        {
            return Decode<T>(raw.ToString());
        }

        public string Encode<T>(T obj) where T : GameObject
        {
            return Encode(typeof(T), obj);
        }

        public Block DecodeBlock(string raw)
        {
            v2.Clear();
            var values = Parse(raw, ",", v2);
            if (!values.TryGetValue("1", out var rawId))
                throw new Exception("Raw data doesn't contains id component");
            if (!int.TryParse(rawId, out var id))
                throw new Exception("Id is not a number");
            var type = GeometryDashApi.GetBlockType(id);

            return (Block) Decode(type, values, (GameObject) Activator.CreateInstance(type));
        }

        public string EncodeBlock(Block block)
        {
            return Encode(block.GetType(), block);
        }

        public GameObject Decode(Type type, string raw)
        {
            var instance = (GameObject) Activator.CreateInstance(type);
            v3.Clear();
            return Decode(type, Parse(raw, instance.GetParserSense(), v3), instance);
        }

        private static GameObject Decode(Type type, Dictionary<string, string> values, GameObject instance)
        {
            var descriptor = descriptors.GetOrCreate(type, CreateDescriptor);
            
            foreach (var item in values)
            {
                var key = item.Key;
                var value = item.Value;
                descriptor.Set(instance, int.Parse(key), value.AsSpan());

                // if (member.ArraySeparatorAttribute != null)
                // {
                //     member.SetValue(instance, ArrayParser.Decode(member.MemberType, member.ArraySeparatorAttribute.Separator, value));
                //     continue;
                // }
            }

            return instance;
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

        private static Dictionary<string, string> Parse(string raw, string sense, Dictionary<string, string> values)
        {
            var parser = new LLParserSpan(sense.AsSpan(), raw.AsSpan());

            while (true)
            {
                Span<char> x = null;
                var key = parser.Next();
                var value = parser.Next();
                if (key == null)
                    break;
                if (value == null)
                    throw new Exception("Invalid raw data. Count of components in raw data is odd");
                values[key.ToString()] = value.ToString();
            }

            return values;
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