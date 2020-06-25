using System;

namespace GeometryDashAPI.Server.Enums
{
    public class TopAttribute : Attribute
    {
        public string OriginalName { get; set; }

        public TopAttribute(string originalName)
        {
            OriginalName = originalName;
        }
    }
}
