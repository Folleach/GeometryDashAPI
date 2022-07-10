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
        private readonly TypeDescriptorHelper.Setter<T>[] setters;
        private readonly Dictionary<string, int> mappings;
        private readonly string sense;
        private readonly Func<T> create;
        private readonly bool isStruct;
        private readonly int baseIndex;

        public TypeDescriptor()
        {
            var type = typeof(T);
            create = CreateInstanceExpression<T>(type).Compile();

            var senseAttribute = type.GetCustomAttribute<SenseAttribute>();
            if (senseAttribute == null)
                throw new ArgumentException($"Type '{type}' doesn't contains sense attribute", sense);
            sense = senseAttribute.Sense;

            isStruct = type.GetCustomAttribute<AsStructAttribute>() != null;

            var members = GetPropertiesAndFields(type)
                .Select(member => (member, attribute: member.GetCustomAttribute<GamePropertyAttribute>()))
                .Where(x => x.attribute != null)
                .ToArray();
            var createSetter = typeof(TypeDescriptorHelper)
                .GetMethod(nameof(TypeDescriptorHelper.CreateSetter), BindingFlags.Static | BindingFlags.NonPublic);

            (setters, baseIndex, mappings) = InitSetters(type, members);
            foreach (var (member, attribute) in members)
            {
                var memberType = member.GetMemberType();
                var createSetterGeneric = createSetter!.MakeGenericMethod(memberType, type);
                var setterExpression =
                    (Expression<TypeDescriptorHelper.Setter<T>>)createSetterGeneric.Invoke(null,
                        new object[] { member });
                var setter = setterExpression!.Compile();
                var setterIndex = int.TryParse(attribute.Key, out var value) ? baseIndex + value : attribute.KeyOverride;
                setters[setterIndex] = setter;
            }
        }

        public T Create() => create();

        public T Create(ReadOnlySpan<char> raw)
        {
            var instance = create();
            var parser = new LLParserSpan(sense, raw);
            
            if (isStruct)
            {
                var position = 0;
                Span<char> value;
                while ((value = parser.Next()) != null)
                {
                    if (!TrySet(instance, position++, value))
                        throw new InvalidOperationException("Struct is not fully implemented");
                }
            }
            else
            {
                while (parser.TryParseNext(out var key, out var value))
                {
                    if (!int.TryParse(key, out var index))
                    {
                        var keyString = key.ToString();
                        var mapped = mappings[keyString];
                        if (!TrySet(instance, mapped, value))
                            instance.WithoutLoaded.Add(keyString, value.ToString());
                        continue;
                    }
                    if (!TrySet(instance, baseIndex + index, value))
                        instance.WithoutLoaded.Add(key.ToString(), value.ToString());
                }
            }

            return instance;
        }

        private bool TrySet(IGameObject instance, int key, ReadOnlySpan<char> raw)
        {
            if (key < 0)
                return false;
            var setter = setters[key];
            if (setter == null)
                return false;
            setter((T)instance, raw);
            return true;
        }

        private static Expression<Func<TB>> CreateInstanceExpression<TB>(Type type)
        {
            var ctor = Expression.New(type);
            var memberInit = Expression.MemberInit(ctor);

            return Expression.Lambda<Func<TB>>(memberInit);
        }

        private static (TypeDescriptorHelper.Setter<T>[], int baseIndex, Dictionary<string, int> mappings) InitSetters(Type type, IEnumerable<(MemberInfo member, GamePropertyAttribute attribute)> members)
        {
            var keys = new HashSet<string>();
            var maxKeyValue = 0;
            Dictionary<string, int> mappings = null;
            foreach (var (member, attribute) in members)
            {
                if (int.TryParse(attribute.Key, out var key))
                {
                    if (keys.Contains(attribute.Key))
                        throw new InvalidOperationException($"Type is invalid. Has same keys: {key}");
                    keys.Add(attribute.Key);
                    if (maxKeyValue < key)
                        maxKeyValue = key;
                    continue;
                }

                mappings ??= new Dictionary<string, int>();
                if (attribute.KeyOverride == -1)
                    throw new InvalidOperationException($"Key override for member '{attribute.Key}' in {type.Name} is not set");
                mappings.Add(attribute.Key, attribute.KeyOverride);
            }

            if (mappings != null)
            {
                var values = mappings.Select(x => x.Value).ToHashSet();
                for (var i = 0; i < mappings.Count; i++)
                {
                    if (!values.Contains(i))
                        throw new InvalidOperationException($"Be careful with your memory! Use incremental keyOverride property which starts with 0. Miss take on: {i}");
                }
            }
            
            var baseIndex = mappings?.Count ?? 0;
            return (new TypeDescriptorHelper.Setter<T>[baseIndex + maxKeyValue + 1], baseIndex, mappings);
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

    public class TypeDescriptorHelper
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
            if (typeof(TProp).IsEnum)
                return CreateEnumSetter<TProp, TInstance>(field, target, data);
            if (typeof(TProp).IsGenericType && typeof(TProp).GetGenericTypeDefinition() == typeof(List<>))
                return CreateArraySetter<TProp, TInstance>(field, target, data, member);
            return CreateParserSetter<TProp, TInstance>(field, data, target);
        }

        private static Expression<Setter<TInstance>> CreateArraySetter<TProp, TInstance>(MemberExpression field, ParameterExpression data, ParameterExpression target, MemberInfo member)
        {
            var arraySeparator = member.GetCustomAttribute<ArraySeparatorAttribute>();
            if (arraySeparator == null)
                throw new InvalidOperationException($"Member {member} doesn't contains {typeof(ArraySeparatorAttribute)}");
            var separatorExpression = Expression.Constant(arraySeparator.Separator);
            var parserInstanceExpression = Expression.Constant(GeometryDashApi.parser);
            var parserInstance = GeometryDashApi.parser;
            var genericDecode = parserInstance.GetType().GetMethod(nameof(parserInstance.DecodeArray), new[] { typeof(ReadOnlySpan<char>), typeof(string) });
            var decode = genericDecode?.MakeGenericMethod(typeof(TProp).GetGenericArguments()[0]);
            if (decode == null)
                throw new InvalidOperationException($"Method T DecodeArray(ReadOnlySpan<char>, string) is not implemented in parser {parserInstance}");
            
            var call = Expression.Call(parserInstanceExpression, decode, target, separatorExpression);
            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Convert(call, typeof(TProp))), data, target
            );
        }

        private static Expression<Setter<TInstance>> CreateParserSetter<TProp, TInstance>(MemberExpression field, ParameterExpression target, ParameterExpression data)
        {
            var parser = GetParserMethod<TProp>(out var instance);
            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Call(instance, parser, target)), data, target
            );
        }

        private static Expression<Setter<TInstance>> CreateEnumSetter<TProp, TInstance>(MemberExpression field, ParameterExpression target, ParameterExpression data)
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(TProp));
            var parser = GetParserMethod(underlyingType, out var instance);
            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Convert
                    (
                        Expression.Call(instance, parser, data), typeof(TProp)
                    )
                ), target, data
            );
        }

        private static MethodInfo GetParserMethod<TProp>(out Expression instanceExpression)
        {
            return GetParserMethod(typeof(TProp), out instanceExpression);
        }

        private static MethodInfo GetParserMethod(Type propType, out Expression instanceExpression)
        {
            if (typeof(IGameObject).IsAssignableFrom(propType))
            {
                instanceExpression = Expression.Constant(GeometryDashApi.parser);
                var parserInstance = GeometryDashApi.parser;
                var genericDecode = parserInstance.GetType().GetMethod(nameof(parserInstance.Decode), new[] { typeof(ReadOnlySpan<char>) });
                var decode = genericDecode?.MakeGenericMethod(propType);

                if (decode == null)
                    throw new InvalidOperationException($"Method T Decode(ReadOnlySpan<char>) is not implemented in parser {parserInstance}");
                return decode;
            }

            var parserType = typeof(Parsers);
            var parserName = GenerateParserName(propType);
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
