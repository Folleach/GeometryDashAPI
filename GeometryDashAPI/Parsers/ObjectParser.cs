using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Parser;

namespace GeometryDashAPI.Parsers
{
    public static class ObjectParser
    {
        private static readonly Dictionary<Type, GameTypeDescription> TypesCache
            = new Dictionary<Type, GameTypeDescription>();

        public static T Decode<T>(string raw) where T : GameObject, new()
        {
            var instance = new T();
            return (T) Decode(typeof(T), Parse(raw, instance.ParserSense), instance);
        }

        public static string Encode<T>(T obj) where T : GameObject
        {
            return Encode(typeof(T), obj);
        }

        public static Block DecodeBlock(string raw)
        {
            var values = Parse(raw, ",");
            if (!values.TryGetValue("1", out var rawId))
                throw new Exception("Raw data doesn't contains id component");
            if (!int.TryParse(rawId, out var id))
                throw new Exception("Id is not a number");
            var type = GeometryDashApi.GetBlockType(id);

            return (Block) Decode(type, values, (GameObject) Activator.CreateInstance(type));
        }

        public static string EncodeBlock(Type type, Block block)
        {
            return Encode(type, block);
        }

        private static GameObject Decode(Type type, Dictionary<string, string> values, GameObject instance)
        {
            var description = GeometryDashApi.GetDescription(type);

            foreach (var item in values)
            {
                var key = item.Key;
                var value = item.Value;
                if (!description.Members.TryGetValue(key, out var member))
                {
                    instance.WithoutLoaded.Add(key, value);
                    continue;
                }

                if (member.IsGameObject)
                {
                    var newGameObject = (GameObject) Activator.CreateInstance(member.MemberType);
                    member.SetValue(instance,
                        Decode(member.MemberType, Parse(value, newGameObject.ParserSense), newGameObject));
                    continue;
                }

                member.SetValue(instance, GetParser(member.MemberType)(value));
            }

            return instance;
        }

        private static string Encode(Type type, GameObject obj)
        {
            var builder = new StringBuilder();
            var description = GeometryDashApi.GetDescription(type);
            var needSeparate = false;

            void AppendKey(string key)
            {
                if (needSeparate)
                    builder.Append(obj.ParserSense);
                needSeparate = true;
                builder.Append(key);
                builder.Append(obj.ParserSense);
            }

            foreach (var member in description.Members.Values)
            {
                if (member.IsGameObject)
                {
                    AppendKey(member.Attribute.Key);
                    builder.Append(Encode(member.MemberType, (GameObject) member.GetValue(obj)));
                    continue;
                }

                var value = member.GetValue(obj);
                if (!member.Attribute.AlwaysSet && Equals(member.Attribute.DefaultValue, value))
                    continue;

                AppendKey(member.Attribute.Key);
                var parser = GeometryDashApi.ObjectToStringParsers[member.MemberType];
                builder.Append(parser(value));
            }

            foreach (var item in obj.WithoutLoaded)
            {
                if (needSeparate)
                    builder.Append(obj.ParserSense);
                builder.Append(item.Key);
                builder.Append(obj.ParserSense);
                builder.Append(item.Value);
            }

            return builder.ToString();
        }

        private static Dictionary<string, string> Parse(string raw, string sense)
        {
            var parser = new LLParser(sense[0], raw);
            var values = new Dictionary<string, string>();

            while (true)
            {
                var key = parser.Next();
                var value = parser.Next();
                if (key == null)
                    break;
                if (value == null)
                    throw new Exception("Invalid raw data. Count of components in raw data is odd");
                values.Add(key.ToString(), value.ToString());
            }

            return values;
        }

        private static Func<string, object> GetParser(Type forType)
        {
            if (GeometryDashApi.StringToObjectParsers.TryGetValue(forType, out var parser))
                return parser;
            throw new Exception($"Couldn't parse: {forType}");
        }
    }
}