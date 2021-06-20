using System;

namespace GeometryDashAPI.Parsers
{
    public static class StructParser
    {
        public static T Decode<T>(string raw) where T : GameStruct, new()
        {
            return (T) Decode(typeof(T), raw, new T());
        }

        internal static GameStruct Decode(Type type, string raw)
        {
            return (GameStruct) Decode(type, raw, (GameStruct)Activator.CreateInstance(type));
        }

        internal static string Encode(Type type, GameStruct value)
        {
            throw new NotImplementedException();
        }

        private static object Decode(Type type, string raw, GameStruct instance)
        {
            var parser = new LLParser(instance.GetParserSense(), raw);
            var description = GeometryDashApi.GetStructMemberCache(type);
            
            Span<char> next = null;
            var position = -1;
            while ((next = parser.Next()) != null)
            {
                position++;
                var value = next.ToString();
                if (!description.Members.TryGetValue(position, out var member))
                    throw new Exception($"Can't find member for position: {position}");

                if (member.ArraySeparatorAttribute != null)
                {
                    member.SetValue(instance, ArrayParser.Decode(member.MemberType, member.ArraySeparatorAttribute.Separator, value));
                    continue;
                }
                
                member.SetValue(instance, GeometryDashApi.GetStringParser(member.MemberType)(value));
            }

            return instance;
        }
    }
}