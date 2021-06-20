using System.Collections.Generic;

namespace GeometryDashAPI.Tests.TestObjects
{
    public class ComplexParserObject : GameStruct
    {
        [StructPosition(0)] public ComplexObject Object { get; set; }

        [StructPosition(1)] public ComplexStruct Struct { get; set; }
        
        [StructPosition(2)]
        [ArraySeparator("~")]
        public List<ComplexObject> ObjectArray { get; set; }
        
        [StructPosition(3)]
        [ArraySeparator("~")]
        public List<ComplexStruct> StructArray { get; set; }

        public override string GetParserSense() => "#";

        public static readonly string ExampleInput = "A:2#10:12#A:1~A:3~A:5#1:2~3:4"; // Из вот этой строки

        // Даёт вот этот объект
        public static readonly ComplexParserObject ExampleExpected = new ComplexParserObject()
        {
            Object = new ComplexObject()
            {
                A = 2
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
    
    public class ComplexStruct : GameStruct
    {
        [StructPosition(0)] public int X { get; set; }
        [StructPosition(1)] public int Y { get; set; }
        
        public override string GetParserSense() => ":";
    }

    public class ComplexObject : GameObject
    {
        [GameProperty("A")]
        public int A { get; set; }

        public override string GetParserSense() => ":";
    }
}