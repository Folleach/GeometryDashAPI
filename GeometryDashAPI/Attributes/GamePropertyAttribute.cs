using System;

namespace GeometryDashAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GamePropertyAttribute : Attribute
    {
        public string Key { get; }
        public object DefaultValue { get; }
        public bool AlwaysSet { get; }
        public int KeyOverride { get; set; }
        public int Order { get; set; } = int.MaxValue;

        public GamePropertyAttribute(string key, object defaultDefaultValue = null, bool alwaysSet = false)
        {
            Key = key;
            DefaultValue = defaultDefaultValue;
            AlwaysSet = alwaysSet;
        }
    }
}
