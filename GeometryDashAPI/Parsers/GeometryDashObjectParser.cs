using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Parser
{
    public class GeometryDashObjectParser
    {
        private readonly Dictionary<Type, GameTypeDescription> typesCache
            = new Dictionary<Type, GameTypeDescription>();
        private readonly Dictionary<Type, Func<string, object>> stringToObjectParsers
            = new Dictionary<Type, Func<string, object>>()
            {
                { typeof(int), x => int.Parse(x) },
                { typeof(double), x => GameConvert.StringToDouble(x) },
                { typeof(string), x => x }
            };
        private readonly Dictionary<Type, Func<object, string>> objectToStringParsers
            = new Dictionary<Type, Func<object, string>>()
            {
                { typeof(int), x => x.ToString() },
                { typeof(double), x => GameConvert.DoubleToString((double)x) },
                { typeof(string), x => (string)x }
            };
        
        public T Decode<T>(string raw) where T : GameObject, new()
        {
            var description = GetDescription(typeof(T));
            var resultObject = new T();
            var parser = new LLParser(resultObject.ParserSense);
            
            var enumerator = parser.Parse(raw).GetEnumerator();
            while (enumerator.MoveNext())
            {
                var key = enumerator.Current;
                if (!enumerator.MoveNext())
                    throw new Exception("Invalid raw data. Count of components in raw data is odd");
                var value = enumerator.Current;
                if (!description.Properties.TryGetValue(key, out var property))
                {
                    resultObject.WithoutLoaded.Add(key, value);
                    continue;
                }
                property.Property.SetValue(resultObject, GetParser(property.Property.PropertyType)(value));
            }

            return resultObject;
        }

        public string Encode<T>(T obj) where T : GameObject
        {
            var builder = new StringBuilder();
            var description = GetDescription(typeof(T));
            var needSeparate = false;
            
            foreach (var property in description.Properties.Values)
            {
                if (needSeparate)
                    builder.Append(obj.ParserSense);
                builder.Append(property.Attribute.Key);
                builder.Append(obj.ParserSense);
                var parser = objectToStringParsers[property.Property.PropertyType];
                builder.Append(parser(property.Property.GetValue(obj)));
                needSeparate = true;
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

        private GameTypeDescription GetDescription(Type type)
        {
            if (typesCache.TryGetValue(type, out var description))
                return description;
            return typesCache[type] = new GameTypeDescription(type);
        }

        private Func<string, object> GetParser(Type forType)
        {
            if (stringToObjectParsers.TryGetValue(forType, out var parser))
                return parser;
            throw new Exception($"Couldn't parse: {forType}");
        }
    }
}