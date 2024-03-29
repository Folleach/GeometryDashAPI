﻿using System.Runtime.Serialization;
using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects
{
    [Sense("#")]
    [AsStruct]
    public class ComplexParserObject : GameObject
    {
        [GameProperty("0")]
        public ComplexObject Object { get; set; }

        [GameProperty("1")] public ComplexStruct Struct { get; set; }
        
        [GameProperty("2")]
        [ArraySeparator("~")]
        public List<ComplexObject> ObjectArray { get; set; }
        
        [GameProperty("3")]
        [ArraySeparator("~")]
        public List<ComplexStruct> StructArray { get; set; }

        public static readonly string ExampleInput = "A:2:1:31:B:23#10:12#A:1~A:3~A:5#1:2~3:4"; // Give from this string

        // this object
        public static readonly ComplexParserObject ExampleExpected = new ComplexParserObject()
        {
            Object = new ComplexObject()
            {
                A = 2,
                X = 31,
                B = 23
            },
            Struct = new ComplexStruct()
            {
                X = 10, Y = 12
            },
            ObjectArray = new List<ComplexObject>()
            {
                new ComplexObject() { A = 1 },
                new ComplexObject() { A = 3 },
                new ComplexObject() { A = 5 }
            },
            StructArray = new List<ComplexStruct>()
            {
                new ComplexStruct() { X = 1, Y = 2},
                new ComplexStruct() { X = 3, Y = 4}
            }
        };
    }

    [AsStruct]
    [Sense(":")]
    public class ComplexStruct : GameObject
    {
        [GameProperty("0")] public int X { get; set; }
        [GameProperty("1")] public int Y { get; set; }
    }

    [Sense(":")]
    public class ComplexObject : GameObject
    {
        [GameProperty("1")]
        public int X { get; set; }
        [GameProperty("A", KeyOverride = 0)]
        public int A { get; set; }
        [GameProperty("B", KeyOverride = 1)]
        public int B { get; set; }
    }
}