using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Parsers
{
    public class Td
    {
        public readonly bool IsGameObject;
        public readonly bool IsGameStruct;
        public readonly bool IsArray;

        public Td(Type type)
        {
            IsGameObject = typeof(GameObject).IsAssignableFrom(type);
            IsGameStruct = typeof(GameStruct).IsAssignableFrom(type);
            IsArray = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}