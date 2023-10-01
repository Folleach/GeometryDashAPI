using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Serialization
{
    public class TypeDescriptor<T> : IDescriptor<T>, ICopyDescriptor<T> where T : IGameObject
    {
        private readonly SetterInfo<T>[] setters;
        private readonly PrinterInfo<T>[] printers;
        private readonly Dictionary<string, int> mappings;
        private readonly string sense;
        private readonly Func<T> create;
        private readonly bool isStruct;
        private readonly int baseIndex;

        public TypeDescriptor()
        {
            var type = typeof(T);
            create = CreateInstanceExpression<T>().Compile();

            var senseAttribute = type.GetCustomAttribute<SenseAttribute>();
            if (senseAttribute == null)
                throw new ArgumentException($"Type '{type}' doesn't contains sense attribute", sense);
            sense = senseAttribute.Sense;

            isStruct = type.GetCustomAttribute<AsStructAttribute>() != null;

            var members = GetPropertiesAndFields(type)
                .Select(member => (member, attribute: member.GetCustomAttribute<GamePropertyAttribute>()))
                .Where(x => x.attribute != null && !x.attribute.IgnoreField)
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
                setters[setterIndex] = new SetterInfo<T>(setter, attribute, member);
            }

            printers = InitPrinters(members);
        }

        /// <summary>
        /// Creates an instance of <see cref="T"/><br/>
        /// Fast way to create instance, because <see cref="TypeDescriptor{T}"/> compile a factory for this
        /// </summary>
        /// <returns>Instance of <see cref="T"/></returns>
        public T Create() => create();

        /// <summary>
        /// Creates an instance of <see cref="T"/> from his string representation<br/>
        /// </summary>
        /// <param name="raw">The string representation</param>
        /// <returns>Instance of <see cref="T"/> with defined values</returns>
        public T Create(ReadOnlySpan<char> raw)
        {
            var instance = create();
            var parser = new LLParserSpan(sense, raw);

            if (isStruct)
            {
                var position = 0;
                ReadOnlySpan<char> value;
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
                        if (!mappings.TryGetValue(keyString, out var mapped))
                        {
                            instance.WithoutLoaded.Add($"{key.ToString()}{sense}{value.ToString()}");
                            continue;
                        }
                        if (!TrySet(instance, mapped, value))
                            instance.WithoutLoaded.Add($"{keyString}{sense}{value.ToString()}");
                        continue;
                    }
                    if (!TrySet(instance, baseIndex + index, value))
                        instance.WithoutLoaded.Add($"{key.ToString()}{sense}{value.ToString()}");
                }
            }

            return instance;
        }

        /// <summary>
        /// Copies <see cref="IGameObject"/> instance as a string view to <see cref="StringBuilder"/>
        /// </summary>
        /// <param name="instance">A <see cref="IGameObject"/></param>
        /// <param name="destination">Destination <see cref="StringBuilder"/></param>
        public void CopyTo(T instance, StringBuilder destination)
        {
            var changed = false;
            if (instance == null)
                return;
            foreach (var info in printers)
            {
                if (!info.Attribute.AlwaysSet && info.IsDefault(instance))
                    continue;
                if (changed)
                    destination.Append(sense);
                changed = true;
                info.Printer(instance, destination);
            }

            foreach (var item in instance.WithoutLoaded)
            {
                if (changed)
                    destination.Append(sense);
                changed = true;
                destination.Append(item);
            }
        }

        private bool TrySet(IGameObject instance, int key, ReadOnlySpan<char> raw)
        {
            if (key < 0)
                return false;
            if (key >= setters.Length)
                return false;
            var info = setters[key];
            if (info.Setter == null)
                return false;
            info.Setter((T)instance, raw);
            return true;
        }

        internal void EncodeArray<TI>(TI[] array, string separator, StringBuilder builder, TypeDescriptorHelper.Printer<TI> getValue)
        {
            var shouldAppendSense = false;
            foreach (var item in array)
            {
                if (shouldAppendSense)
                    builder.Append(sense);
                shouldAppendSense = true;
                // builder.Append(getValue(item));
            }
        }

        private PrinterInfo<T>[] InitPrinters(IEnumerable<(MemberInfo member, GamePropertyAttribute attribute)> members)
        {
            return members
                .OrderBy(x => x.attribute.Order)
                .Select(x =>
                {
                    var printerExp = TypeDescriptorHelper.CreatePrinter<T>(x.member, x.attribute, sense);
                    var isDefaultExp = TypeDescriptorHelper.CreateIsDefaultFunction<T>(x.member, x.attribute);
                    return new PrinterInfo<T>(
                        printerExp.Compile(),
                        isDefaultExp.Compile(),
                        x.attribute)
#if DEBUG
                        {
                            PrinterExp = printerExp,
                            IsDefaultExp = isDefaultExp
                        }
#endif
                    ;
                })
                .ToArray();
        }

        private static Expression<Func<TB>> CreateInstanceExpression<TB>()
        {
            var ctor = Expression.New(typeof(TB));
            var memberInit = Expression.MemberInit(ctor);

            return Expression.Lambda<Func<TB>>(memberInit);
        }

        private static (SetterInfo<T>[], int baseIndex, Dictionary<string, int> mappings) InitSetters(Type type, IEnumerable<(MemberInfo member, GamePropertyAttribute attribute)> members)
        {
            var keys = new HashSet<string>();
            var maxKeyValue = 0;
            Dictionary<string, int> mappings = new();
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

                if (attribute.KeyOverride == -1)
                    throw new InvalidOperationException($"Key override for member '{attribute.Key}' in {type.Name} is not set");
                mappings.Add(attribute.Key, attribute.KeyOverride);
            }

            if (mappings.Any())
            {
                var values = mappings.Select(x => x.Value).ToHashSet();
                for (var i = 0; i < mappings.Count; i++)
                {
                    if (!values.Contains(i))
                        throw new InvalidOperationException($"Be careful with your memory! Use incremental keyOverride property which starts with 0. Miss take on: {i}");
                }
            }

            var baseIndex = mappings.Count;
            return (new SetterInfo<T>[baseIndex + maxKeyValue + 1], baseIndex, mappings);
        }

        private static IEnumerable<MemberInfo> GetPropertiesAndFields(Type type)
        {
            foreach (var property in type.GetProperties())
                yield return property;
            var current = type;
            while (current != null && current != typeof(object))
            {
                foreach (var field in current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                    yield return field;
                current = current.BaseType;
            }
        }
    }

    public class TypeDescriptorHelper
    {
        private static readonly string PredefinedMethodPrefix = "GetOrDefault";

        internal delegate void Setter<in TI>(TI instance, ReadOnlySpan<char> raw);
        internal delegate void Printer<in TI>(TI instance, StringBuilder builder);
        internal delegate TOut Getter<in TI, out TOut>(TI instance);

        internal static Expression<Setter<TInstance>> CreateSetter<TProp, TInstance>(MemberInfo member)
        {
            var target = Expression.Parameter(typeof(TInstance));
            var data = Expression.Parameter(typeof(ReadOnlySpan<char>));
            var field = CreateField(member, target);
            if (typeof(TProp).IsEnum)
                return CreateEnumSetter<TProp, TInstance>(field, target, data);
            if (typeof(TProp).IsGenericType && typeof(TProp).GetGenericTypeDefinition() == typeof(List<>))
                return CreateListSetter<TProp, TInstance>(field, target, data, member);
            if (typeof(TProp).IsArray)
                return CreateArraySetter<TProp, TInstance>(field, target, data, member);
            return CreateParserSetter<TProp, TInstance>(field, data, target);
        }

        internal static Expression<Printer<TInstance>> CreatePrinter<TInstance>(MemberInfo member, GamePropertyAttribute attribute, string sense)
        {
            var printerParameterInstance = Expression.Parameter(typeof(TInstance), "instance");
            var printerParameterBuilder = Expression.Parameter(typeof(StringBuilder), "builder");

            var memberType = member.GetMemberType();
            var field = CreateField(member, printerParameterInstance);
            
            
            var append = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new[] { typeof(ReadOnlySpan<char>) });
            if (append == null)
                throw new InvalidOperationException($"method '{nameof(StringBuilder.Append)}' is not contains in {typeof(StringBuilder)}");

            // destination.Append(key);
            var appendKeyCall = Expression.Call(printerParameterBuilder, append, Expression.Convert(Expression.Constant(attribute.Key), typeof(ReadOnlySpan<char>)));
            // destination.Append(separator);
            var appendSenseCall = Expression.Call(printerParameterBuilder, append, Expression.Convert(Expression.Constant(sense), typeof(ReadOnlySpan<char>)));
            // destination.Append(value);
            var appendValueCall = CreateAppendValueCall(memberType, field, printerParameterBuilder, append, member);

            Expression print = Expression.Block(appendKeyCall, appendSenseCall, appendValueCall);
            return Expression.Lambda<Printer<TInstance>>(
                print,
                printerParameterInstance,
                printerParameterBuilder);
        }

        internal static Expression<Getter<TInstance, bool>> CreateIsDefaultFunction<TInstance>(MemberInfo member, GamePropertyAttribute attribute)
            where TInstance : IGameObject
        {
            var printerParameterInstance = Expression.Parameter(typeof(TInstance), "instance");
            var field = CreateField(member, printerParameterInstance);
            var memberType = member.GetMemberType();

            Expression defaultValue = Expression.Constant(attribute.DefaultValue);
            if (memberType.IsValueType && attribute.DefaultValue == null)
                defaultValue = Expression.Default(memberType);
            else if (memberType.IsValueType && !attribute.DefaultValue.GetType().IsValueType)
                throw new InvalidOperationException(
                    $"can not compare property with their default value: {attribute.Key} in {typeof(TInstance).Name}.{memberType.Name}");
            else
                defaultValue = Expression.Convert(defaultValue, memberType);
            var body = Expression.Equal(field, defaultValue);

            return Expression.Lambda<Getter<TInstance, bool>>(body, printerParameterInstance);
        }

        private static MemberExpression CreateField(MemberInfo member, ParameterExpression target)
        {
            return member switch
            {
                PropertyInfo propertyInfo => Expression.Property(target, propertyInfo),
                FieldInfo fieldInfo => Expression.Field(target, fieldInfo),
                _ => throw new ArgumentException($"Not supported member ({member})")
            };
        }

        private static ArraySeparatorAttribute GetArraySeparator(MemberInfo member)
        {
            var arraySeparator = member.GetCustomAttribute<ArraySeparatorAttribute>();
            if (arraySeparator == null)
                throw new InvalidOperationException($"Member {member} doesn't contains {typeof(ArraySeparatorAttribute)}");
            return arraySeparator;
        }

        private static (ConstantExpression serializerExp, ObjectSerializer serializer) GetSerializer()
        {
            var serializerExp = Expression.Constant(GeometryDashApi.Serializer);
            var serializer = GeometryDashApi.Serializer;
            return (serializerExp, serializer);
        }

        #region PRINTERS

        private static Expression CreateAppendValueCall(
            Type type,
            Expression field,
            ParameterExpression printerParameterBuilder,
            MethodInfo append,
            MemberInfo member)
        {
            if (typeof(IGameObject).IsAssignableFrom(type))
                return CreateGameObjectPrinter(type, field, printerParameterBuilder);
            if (type.IsEnum)
                return CreateEnumPrinter(type, field, append, printerParameterBuilder);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var toArrayMethod = type.GetMethod(nameof(List<object>.ToArray), BindingFlags.Public | BindingFlags.Instance);
                if (toArrayMethod == null)
                    throw new InvalidOperationException($"type '{type}' is not contains '{nameof(List<object>.ToArray)}' method");
                return CreateArrayPrinter(
                    type,
                    Expression.Call(field, toArrayMethod), // <-- here is a call `ToArray()`, because I am too lazy to create different implementation
                    printerParameterBuilder,               //      for T[] and List<T>. And I think it's not really necessary.
                    append,                                //      Yes, a little more memory will be consumed, but that's it...
                    member,
                    type.GetGenericArguments()[0]);
            }
            if (type.IsArray)
                return CreateArrayPrinter(type, field, printerParameterBuilder, append, member, type.GetElementType());

            return CreateSimpleValueAppender(type, printerParameterBuilder, append, field);
        }

        private static Expression CreateGameObjectPrinter(Type type, Expression field,
            ParameterExpression printerParameterBuilder)
        {
            var (serializerExp, serializer) = GetSerializer();
            var genericGetDescriptor = serializer.GetType()
                .GetMethod(nameof(serializer.GetDescriptor), BindingFlags.Instance | BindingFlags.NonPublic);
            var getDescriptor = genericGetDescriptor?.MakeGenericMethod(type);
            var descriptor = Expression.Call(serializerExp, getDescriptor);

            var descriptorType = typeof(TypeDescriptor<>).MakeGenericType(type);
            var copyTo = descriptorType.GetMethod("CopyTo");
            return Expression.Call(Expression.Convert(descriptor, descriptorType), copyTo, field, printerParameterBuilder);
        }

        private static Expression CreateEnumPrinter(
            Type type, // todo: use enum type to select the type instead of Int32 constant
            Expression field,
            MethodInfo append,
            ParameterExpression printerParameterBuilder)
        {
            var valuePrinter = typeof(Printers).GetMethod($"{PredefinedMethodPrefix}_{nameof(Int32)}__", BindingFlags.Static | BindingFlags.Public);
            return Expression.Call(printerParameterBuilder, append, Expression.Call(valuePrinter, Expression.Convert(field, typeof(int))));
        }

        private static Expression CreateArrayPrinter(Type type, Expression array, ParameterExpression destination,
            MethodInfo append, MemberInfo member, Type elementType)
        {
            var printArray = typeof(Printers)
                .GetMethod(nameof(Printers.PrintArray))
                ?.MakeGenericMethod(elementType);
            if (printArray == null)
                throw new InvalidOperationException($"{nameof(Printers.PrintArray)} is not exists in class {nameof(Printers)}");
            var arraySense = GetArraySeparator(member);
            var separator = Expression.Constant(arraySense.Separator);
            var instanceParameter = Expression.Parameter(elementType, "arrayItem");
            var appendValueCall = CreateAppendValueCall(elementType, instanceParameter, destination, append, member);
            Expression appendValueLambda = Expression.Lambda(typeof(Printers.PrinterAppend<>).MakeGenericType(elementType), appendValueCall, instanceParameter, destination);

            Expression body = Expression.Call(
                printArray,
                array,
                separator,
                destination,
                appendValueLambda);
            if (arraySense.SeparatorAtTheEnd)
                body = Expression.Block(body, Expression.Call(destination, append, Expression.Convert(separator, typeof(ReadOnlySpan<char>))));
            return body;
        }

        private static Expression CreateSimpleValueAppender(
            Type type,
            ParameterExpression printerParameterBuilder,
            MethodInfo append,
            Expression getValue)
        {
            var valuePrinter = typeof(Printers).GetMethod(GeneratePredefinedName(type), BindingFlags.Static | BindingFlags.Public);
            if (valuePrinter == null)
                throw new InvalidOperationException($"can't find predefined method '{GeneratePredefinedName(type)}' in {nameof(Printers)}");
            return Expression.Call(printerParameterBuilder, append, Expression.Call(valuePrinter, getValue));
        }

        #endregion

        #region SETTERS

        private static Expression<Setter<TInstance>> CreateArraySetter<TProp, TInstance>(MemberExpression field, ParameterExpression target, ParameterExpression data, MemberInfo member)
        {
            var arraySeparator = GetArraySeparator(member);
            var arrayType = typeof(TProp).GetElementType();
            var separatorExpression = Expression.Constant(arraySeparator.Separator);
            var (serializerExp, serializer) = GetSerializer();
            var genericDecode = serializer.GetType().GetMethod(nameof(serializer.GetArray));
            var decode = genericDecode?.MakeGenericMethod(arrayType);
            if (decode == null)
                throw new InvalidOperationException($"Method T[] DecodeArray(ReadOnlySpan<char>, string) is not implemented in parser {serializer}");
            var parser = GetParserMethod(arrayType, out _);
            var concreteDelegate = typeof(IGameSerializer.Parser<>).MakeGenericType(arrayType);
            var call = Expression.Call(serializerExp, decode, data, separatorExpression, Expression.Constant(Delegate.CreateDelegate(concreteDelegate, parser)));
            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Convert(call, typeof(TProp))), target, data
            );
        }

        private static Expression<Setter<TInstance>> CreateListSetter<TProp, TInstance>(MemberExpression field, ParameterExpression target, ParameterExpression data, MemberInfo member)
        {
            var arraySeparator = GetArraySeparator(member);
            var separatorExpression = Expression.Constant(arraySeparator.Separator);
            var (serializerExp, serializer) = GetSerializer();
            var genericDecode = serializer.GetType().GetMethod(nameof(serializer.DecodeList), new[] { typeof(ReadOnlySpan<char>), typeof(string) });
            var decode = genericDecode?.MakeGenericMethod(typeof(TProp).GetGenericArguments()[0]);
            if (decode == null)
                throw new InvalidOperationException($"Method List<T> DecodeList(ReadOnlySpan<char>, string) is not implemented in parser {serializer}");
            
            var call = Expression.Call(serializerExp, decode, data, separatorExpression);
            return Expression.Lambda<Setter<TInstance>>
            (
                Expression.Assign(field, Expression.Convert(call, typeof(TProp))), target, data
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

        private static MethodInfo GetParserMethod<TProp>(out Expression? instanceExpression)
        {
            return GetParserMethod(typeof(TProp), out instanceExpression);
        }

        private static MethodInfo GetParserMethod(Type propType, out Expression? serializerExp)
        {
            if (typeof(IGameObject).IsAssignableFrom(propType))
            {
                var (exp, serializer) = GetSerializer();
                serializerExp = exp;
                var genericDecode = serializer.GetType().GetMethod(nameof(serializer.Decode), new[] { typeof(ReadOnlySpan<char>) });
                var decode = genericDecode?.MakeGenericMethod(propType);

                if (decode == null)
                    throw new InvalidOperationException($"Method T Decode(ReadOnlySpan<char>) is not implemented in parser {serializer}");
                return decode;
            }

            var parserType = typeof(Parsers);
            var parserName = GeneratePredefinedName(propType);
            var parser = parserType.GetMethod(parserName, BindingFlags.Static | BindingFlags.Public);

            serializerExp = null;
            if (parser == null)
                throw new InvalidOperationException($"Parser '{parserName}' isn't present");
            return parser;
        }

        private static string GeneratePredefinedName(Type type)
        {
            if (type.IsGenericType)
                return $"{PredefinedMethodPrefix}_{type.GetGenericArguments()[0].Name}_Y";
            return $"{PredefinedMethodPrefix}_{type.Name}__";
        }

        #endregion
    }
}
