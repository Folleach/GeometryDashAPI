using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Responses;
using JetBrains.Profiler.Api;
using Newtonsoft.Json;

namespace Examples
{
    class TestObject
    {
        public int Value { get; set; }
        public string X { get; set; }
    }
    
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            
            F();
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

        private static Expression<Func<T>> CreateInstance<T>() { 
            var createdType = typeof(T);
            var ctor = Expression.New(createdType);
            var memberInit = Expression.MemberInit(ctor);

            return
                Expression.Lambda<Func<T>>(memberInit);
        }

        private static Expression<Action<TInstance, T>> CreateSetter<T, TInstance>(MemberInfo member)
        {
            var target = Expression.Parameter(typeof(TInstance));
            MemberExpression getter;
            if (member is PropertyInfo propertyInfo)
                getter = Expression.Property(target, propertyInfo);
            else if (member is FieldInfo fieldInfo)
                getter = Expression.Field(target, fieldInfo);
            else
                throw new ArgumentException("Not supported member");
            
            var property = Expression.Parameter(typeof(T));
            return Expression.Lambda<Action<TInstance, T>>
            (
                Expression.Assign(getter, property), target, property
            );
        }

        private static Regex IdRegex = new Regex(@"1,(?<t>\d)", RegexOptions.Compiled);

        private static Dictionary<int, Type> blocks = new Dictionary<int, Type>()
        {
            [1] = typeof(BaseBlock)
        };

        private static Type GetBlockType(ReadOnlySpan<char> raw)
        {
            return int.TryParse(IdRegex.Match(raw.ToString()).Groups["t"].Value, out var value) ? blocks[value] : typeof(BaseBlock);
        }

        private static T Create<T>() where T : struct => new();

        private static string Value;
        
        private static void F()
        {
            PerformanceTest();
            return;
            // var str = "28,10.1,29,1111,100,20";
            // var descriptor = new TypeDescriptor<MoveTrigger, int>();
            // var trigger = descriptor.Create(str);
            //
            // Expression<Func<float>> func = () => trigger.MoveX;
            //
            // Console.WriteLine(func);
            
            // var type = typeof(MoveTrigger);
            // var propertyMoveX = type.GetProperty("MoveY", BindingFlags.Instance | BindingFlags.Public);
            // var trigger = (MoveTrigger)Activator.CreateInstance(type);
            // propertyMoveX.SetValue(trigger, 202.1f);
            
            // 1 способ, обычный код
            // var trigger = new MoveTrigger();
            // trigger.MoveX = 10.1f;
            // trigger.MoveY = 1f;
        
            // var descriptors = new List<IDescriptor<IGameObject, int>>();
            // var descriptor = new TypeDescriptor<MoveTrigger, int>();
            // descriptors.Add(descriptor);
            // var instance = descriptor.Create();
            // descriptor.Set(instance, 29, "12.4".AsSpan());

            // var type = typeof(MoveTrigger);
            // var createSetter = typeof(Program).GetMethod(nameof(CreateSetter), BindingFlags.Static | BindingFlags.NonPublic);
            // var instance = CreateInstance<MoveTrigger>().Compile()();
            // var members = GetPropertiesAndFields(type);
            // foreach (var member in members)
            // {
            //     Type returnType = null;
            //     if (member is PropertyInfo propertyInfo)
            //         returnType = propertyInfo.PropertyType;
            //     if (member is FieldInfo fieldInfo)
            //         returnType = fieldInfo.FieldType;
            //     var reflectionCreateSetter = createSetter!.MakeGenericMethod(returnType!, type);
            //     var exp = reflectionCreateSetter.Invoke(null, new []{ member });
            // }
            // var setter = CreateSetter<float, MoveTrigger>(type.GetProperty(nameof(MoveTrigger.MoveX))).Compile();
            // setter(instance, 32);
        }

        private static void PerformanceTest()
        {
            Console.WriteLine("start");
            MemoryProfiler.GetSnapshot("Before run");
            var levelRaw = File.ReadAllText(@"C:\Users\Andrey\Documents\GitHub\GeometryDashAPI\cromulent.txt");
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            var level = new Level(response.GetResultOrDefault().Level.LevelString, true);
            MemoryProfiler.GetSnapshot("After run");
            GC.Collect(2);
            MemoryProfiler.GetSnapshot("After GC");
            Console.WriteLine($"done\tblocks: {level.Blocks.Count}, colors: {level.Colors.Count}");
            Console.ReadKey();
        }
    }
}
