using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeometryDashAPI.Levels
{
    public class BindingBlockID : Dictionary<int, ConstructorInfo>
    {
        public void Bind(int id, Type type)
        {
            if (type.GetInterface(nameof(IBlock)) == null)
                throw new NotImplementIBlockException(type);
            ConstructorInfo ctrInfo = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public, null,
                CallingConventions.HasThis, new Type[] { typeof(string[]) }, null);
            if (ctrInfo != null)
                Add(id, ctrInfo);
            else
                throw new ConstructorNotFoundException(type);
        }

        public T Invoke<T>(int id, string[] data)
        {
            return (T)base[id].Invoke(new object[] { data });
        }
    }
}
