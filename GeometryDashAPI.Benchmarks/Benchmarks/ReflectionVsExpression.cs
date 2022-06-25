using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class ReflectionVsExpression
{
    private static readonly Type type = typeof(InitObject);
    private static readonly Func<InitObject> CreateInitObjectByExpression = BuildLambda().Compile();

    private static readonly PropertyInfo nameProperty = type.GetProperty(nameof(InitObject.Name));
    private static readonly PropertyInfo xProperty = type.GetProperty(nameof(InitObject.X));

    private static readonly Action<InitObject, string> namePropertyExp = CreateSetter<string>(nameof(InitObject.Name)).Compile();
    private static readonly Action<InitObject, int> xPropertyExp = CreateSetter<int>(nameof(InitObject.X)).Compile();

    private static readonly Func<string, int, InitObject> creator =
        CreateWithInit<string, int>(nameof(InitObject.Name), nameof(InitObject.X)).Compile();

    private static readonly string KeySet = ".123.";

    private static readonly string JustObject = "abcdef";

    private static readonly TypeDescriptor<MoveTrigger, int> descriptor = new();
    private static readonly string ValueToSet = "20.2";

    private static readonly PropertyInfo MoveXProperty = typeof(MoveTrigger).GetProperty(nameof(MoveTrigger.MoveX), BindingFlags.Instance | BindingFlags.Public);

    private static readonly Dictionary<string, string> StringKeys = new()
    {
        ["123"] = "a",
        ["321"] = "b"
    };

    private static readonly Dictionary<int, string> IntKeys = new()
    {
        [123] = "a",
        [321] = "b"
    };

    [Benchmark]
    public float DefaultSet()
    {
        var instance = new MoveTrigger();
        var value = GameConvert.StringToSingle(ValueToSet);
        instance.MoveX = value;
        return instance.MoveX;
    }
    
    [Benchmark]
    public float DescriptorSet()
    {
        var instance = descriptor.Create();
        descriptor.Set(instance, 28, ValueToSet.AsSpan());
        return instance.MoveX;
    }
    
    [Benchmark]
    public float ReflectionSet()
    {
        var instance = Activator.CreateInstance<MoveTrigger>();
        MoveXProperty.SetValue(instance, GameConvert.StringToSingle(ValueToSet));
        return instance.MoveX;
    }

    [Benchmark]
    public object JustObjectReturn()
    {
        return JustObject;
    }
    
    [Benchmark]
    public object Boxing()
    {
        return 123;
    }
    
    [Benchmark]
    public string Key_GetByInt()
    {
        IntKeys.TryGetValue(123, out var value);
        return value;
    }
    
    [Benchmark]
    public string Key_GetByString()
    {
        return StringKeys.TryGetValue("123", out var value) ? value : string.Empty;
    }
    
    [Benchmark]
    public int Key_ToInt()
    {
        var span = KeySet.AsSpan(1, 3);
        return int.TryParse(span, out var key) ? key : 0;
    }
    
    [Benchmark]
    public string Key_ToString()
    {
        var span = KeySet.AsSpan(1, 3);
        return span.ToString();
    }
    
    [Benchmark]
    public InitObject Init_Reflection()
    {
        return (InitObject)Activator.CreateInstance(type);
    }
    
    [Benchmark]
    public InitObject Init_Expression()
    {
        return CreateInitObjectByExpression.Invoke();
    }
    
    [Benchmark]
    public void Set_Reflection()
    {
        var instance = CreateInitObjectByExpression.Invoke();
        nameProperty.SetValue(instance, "Hello world");
        xProperty.SetValue(instance, 123);
    }
    
    [Benchmark]
    public void Set_Expression()
    {
        var instance = CreateInitObjectByExpression.Invoke();
        namePropertyExp(instance, "Hello world");
        xPropertyExp(instance, 123);
    }
    
    [Benchmark]
    public InitObject CreateWithInit_Reflection()
    {
        var instance = (InitObject)Activator.CreateInstance(type);
        nameProperty.SetValue(instance, "Hello world");
        xProperty.SetValue(instance, 123);
        return instance;
    }
    
    [Benchmark]
    public InitObject CreateWithInit_Expression()
    {
        return creator("Hello world", 123);
    }
    
    private static Expression<Func<InitObject>> BuildLambda() { 
        var createdType = typeof(InitObject);
        var ctor = Expression.New(createdType);
        var memberInit = Expression.MemberInit(ctor);

        return
            Expression.Lambda<Func<InitObject>>(memberInit);
    }
    
    private static Expression<Action<InitObject, T>> CreateSetter<T>(string propertyName)
    {
        var target = Expression.Parameter(typeof(InitObject));
        var property = Expression.Parameter(typeof(T), propertyName);
        var getter = Expression.Property(target, propertyName);
            
        return Expression.Lambda<Action<InitObject, T>>
        (
            Expression.Assign(getter, property), target, property
        );
    }

    private static Expression<Func<T, T2, InitObject>> CreateWithInit<T, T2>(string firstName, string secondName)
    {
        var target = typeof(InitObject);
        var first = Expression.Parameter(typeof(T), firstName);
        var second = Expression.Parameter(typeof(T2), secondName);
        var exp = Expression.MemberInit(Expression.New(typeof(InitObject)), new []
        {
            Expression.Bind(target.GetProperty(firstName), first),
            Expression.Bind(target.GetProperty(secondName), second)
        });
        return Expression.Lambda<Func<T, T2, InitObject>>(exp, first, second);
    }
}

public class InitObject
{
    public int X { get; set; }
    public string Name { get; set; }
}
