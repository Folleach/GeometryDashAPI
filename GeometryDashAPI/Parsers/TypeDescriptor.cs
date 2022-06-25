using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GeometryDashAPI.Parsers
{
    public class TypeDescriptor<T, TKey> : IDescriptor<T, TKey> where T : IGameObject
    {
        private readonly Dictionary<int, TypeDescriptorHelper.Setter<T>> setters = new();
        private readonly Func<T> create;

        public TypeDescriptor()
        {
            var type = typeof(T);
            var members = GetPropertiesAndFields(type)
                .Select(member => (member, attribute: member.GetCustomAttribute<GamePropertyAttribute>()))
                .Where(x => x.attribute != null);
            create = CreateInstanceExpression<T>(type).Compile();
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
            var parserType = typeof(Parsers);
            var parserName = GenerateParserName(typeof(TProp));
            var parser = parserType.GetMethod(parserName, BindingFlags.Static | BindingFlags.Public);
            if (parser == null)
                throw new InvalidOperationException($"Can't create expression because parser '{parserName}' isn't present");

            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Call(null, parser, data)), target, data
            );
        }

        private static string GenerateParserName(Type type)
        {
            if (type.IsGenericType)
                return $"{ParserOptionsName}_{type.GetGenericArguments()[0].Name}_Y";
            return $"{ParserOptionsName}_{type.Name}__";
        }
    }
}
