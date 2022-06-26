using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Parsers
{
    public class TypeDescriptor<T, TKey> : IDescriptor<T, TKey> where T : IGameObject
    {
        private readonly Dictionary<int, TypeDescriptorHelper.Setter<T>> setters = new();
        private readonly string Sense;
        private readonly Func<T> create;

        public TypeDescriptor()
        {
            var type = typeof(T);
            var members = GetPropertiesAndFields(type)
                .Select(member => (member, attribute: member.GetCustomAttribute<GamePropertyAttribute>()))
                .Where(x => x.attribute != null);
            create = CreateInstanceExpression<T>(type).Compile();
            var senseAttribute = type.GetCustomAttribute<SenseAttribute>();
            if (senseAttribute == null)
                throw new ArgumentException($"Type '{type}' doesn't contains sense attribute", Sense);
            Sense = senseAttribute.Sense;
            var createSetter = typeof(TypeDescriptorHelper)
                .GetMethod(nameof(TypeDescriptorHelper.CreateSetter), BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var (member, attribute) in members)
            {
                var memberType = member.GetMemberType();
                var createSetterGeneric = createSetter!.MakeGenericMethod(memberType, type);
                var setterExpression =
                    (Expression<TypeDescriptorHelper.Setter<T>>)createSetterGeneric.Invoke(null,
                        new object[] { member });
                var setter = setterExpression!.Compile();
                setters.Add(int.Parse(attribute.Key), setter);
            }
        }

        public T Create() => create();

        public T Create(ReadOnlySpan<char> raw)
        {
            var parser = new LLParserSpan(Sense, raw);
            var instance = create();
            Span<char> key;
            while ((key = parser.Next()) != null)
            {
                var value = parser.Next();
                if (value == null)
                    throw new InvalidOperationException($"Object '{raw.ToString()}' has odd number of nodes");
                Set(instance, int.Parse(key), value);
            }

            return instance;
        }

        public void Set(IGameObject instance, int key, ReadOnlySpan<char> raw)
        {
            if (setters.TryGetValue(key, out var setter))
            {
                setter((T)instance, raw);
                return;
            }

            instance.WithoutLoaded.Add(key.ToString(), raw.ToString());
        }

        private static Expression<Func<TB>> CreateInstanceExpression<TB>(Type type)
        {
            var ctor = Expression.New(type);
            var memberInit = Expression.MemberInit(ctor);

            return Expression.Lambda<Func<TB>>(memberInit);
        }

        private static IEnumerable<MemberInfo> GetPropertiesAndFields(Type type)
        {
            foreach (var property in type.GetProperties())
                yield return property;
            var current = type;
            while (current != null && current != typeof(object))
            {
                foreach (var field in current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    yield return field;
                current = current.BaseType;
            }
        }
    }

    internal class TypeDescriptorHelper
    {
        private static readonly string ParserOptionsName = "GetOrDefault";

        internal delegate void Setter<in TI>(TI instance, ReadOnlySpan<char> raw);

        internal static Expression<Setter<TInstance>> CreateSetter<TProp, TInstance>(MemberInfo member)
        {
            var target = Expression.Parameter(typeof(TInstance));
            var data = Expression.Parameter(typeof(ReadOnlySpan<char>));
            var field = member switch
            {
                PropertyInfo propertyInfo => Expression.Property(target, propertyInfo),
                FieldInfo fieldInfo => Expression.Field(target, fieldInfo),
                _ => throw new ArgumentException($"Not supported member ({member})")
            };
            var parser = GetParser<TProp>(out var instance);

            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Call(instance, parser, data)), target, data
            );
        }

        private static MethodInfo GetParser<TProp>(out Expression instanceExpression)
        {
            if (typeof(IGameObject).IsAssignableFrom(typeof(TProp)))
            {
                instanceExpression = Expression.Constant(GeometryDashApi.parser);
                var parserInstance = GeometryDashApi.parser;
                var genericDecode = parserInstance.GetType().GetMethod(nameof(parserInstance.Decode), new[] { typeof(ReadOnlySpan<char>) });
                var decode = genericDecode?.MakeGenericMethod(typeof(TProp));

                if (decode == null)
                    throw new InvalidOperationException($"Method T Decode(ReadOnlySpan<char>) is not implemented in parser {parserInstance}");
                return decode;
            }

            var parserType = typeof(Parsers);
            var parserName = GenerateParserName(typeof(TProp));
            var parser = parserType.GetMethod(parserName, BindingFlags.Static | BindingFlags.Public);

            instanceExpression = null;
            if (parser == null)
                throw new InvalidOperationException($"Parser '{parserName}' isn't present");
            return parser;
        }

        private static string GenerateParserName(Type type)
        {
            if (type.IsGenericType)
                return $"{ParserOptionsName}_{type.GetGenericArguments()[0].Name}_Y";
            return $"{ParserOptionsName}_{type.Name}__";
        }
    }
}
