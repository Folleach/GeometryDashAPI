using System;

namespace GeometryDashAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class GamePropertyAttribute : Attribute
    {
        public string Key { get; }
        public object DefaultValue { get; }
        public bool AlwaysSet { get; }
        public int KeyOverride { get; }

        public GamePropertyAttribute(string key, object defaultDefaultValue = null, bool alwaysSet = false, int keyOverride = -1)
        {
            Key = key;
            DefaultValue = defaultDefaultValue;
            AlwaysSet = alwaysSet;
            KeyOverride = keyOverride;
        }
    }
}
