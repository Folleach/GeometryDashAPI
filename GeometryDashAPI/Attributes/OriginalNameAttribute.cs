using System;

namespace GeometryDashAPI.Attributes
{
    public class OriginalNameAttribute : Attribute
    {
        public string OriginalName { get; set; }

        public OriginalNameAttribute(string originalName)
        {
            OriginalName = originalName;
        }
    }
}
