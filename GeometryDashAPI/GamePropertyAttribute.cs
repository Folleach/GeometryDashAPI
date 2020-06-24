using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GamePropertyAttribute : Attribute
    {
        public string Key { get; private set; }
        public object Value { get; private set; }
        public bool AlwaysSet { get; private set; }

        public GamePropertyAttribute(string key, object defaultValue, bool alwaysSet = false)
        {
            Key = key;
            Value = defaultValue;
            AlwaysSet = alwaysSet;
        }
    }
}
