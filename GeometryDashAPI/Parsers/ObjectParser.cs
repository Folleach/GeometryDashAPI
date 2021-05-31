using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Parser
{
    public class ObjectParser
    {
        private readonly Dictionary<Type, GameTypeDescription> typesCache
            = new Dictionary<Type, GameTypeDescription>();

        public static T Decode<T>(string raw) where T : GameObject, new()
        {
            return (T) Decode(typeof(T), raw, new T());
        }

        public static string Encode<T>(T obj) where T : GameObject
        {
            return Encode(typeof(T), obj);
        }

        private static object Decode(Type type, string raw, GameObject instance = null)
        {
            var description = GeometryDashApi.GetDescription(type);
            var resultObject = instance ?? (GameObject)Activator.CreateInstance(type);
            var parser = new LLParser(resultObject.ParserSense);
            
            var enumerator = parser.Parse(raw).GetEnumerator();
            while (enumerator.MoveNext())
            {
                var key = enumerator.Current;
                if (!enumerator.MoveNext())
                    throw new Exception("Invalid raw data. Count of components in raw data is odd");
                var value = enumerator.Current;
                if (!description.Members.TryGetValue(key, out var member))
                {
                    resultObject.WithoutLoaded.Add(key, value);
                    continue;
                }

                if (member.IsGameObject)
                {
                    member.SetValue(resultObject, Decode(member.MemberType, value));
                    continue;
                }
                
                member.SetValue(resultObject, GetParser(member.MemberType)(value));
            }

            return resultObject;
        }

        private static string Encode(Type type, GameObject obj)
        {
            var builder = new StringBuilder();
            var description = GeometryDashApi.GetDescription(type);
            var needSeparate = false;

            foreach (var member in description.Members.Values)
            {
                if (needSeparate)
                    builder.Append(obj.ParserSense);
                needSeparate = true;
                builder.Append(member.Attribute.Key);
                builder.Append(obj.ParserSense);
                
                if (member.IsGameObject)
                {
                    builder.Append(Encode(member.MemberType, (GameObject)member.GetValue(obj)));
                    continue;
                }
                
                var parser = GeometryDashApi.ObjectToStringParsers[member.MemberType];
                builder.Append(parser(member.GetValue(obj)));
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

        private static Func<string, object> GetParser(Type forType)
        {
            if (GeometryDashApi.StringToObjectParsers.TryGetValue(forType, out var parser))
                return parser;
            throw new Exception($"Couldn't parse: {forType}");
        }
    }
}