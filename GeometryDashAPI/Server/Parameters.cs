using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server
{
    public class Parameters : List<Property>
    {
        public override string ToString()
        {
            var requestContent = new StringBuilder();
            var first = true;
            foreach (var property in this)
            {
                if (!first)
                    requestContent.Append('&');
                AppendProperty(requestContent, property);
                first = false;
            }
            
            return requestContent.ToString();
        }

        private void AppendProperty(StringBuilder builder, Property property)
        {
            builder.Append(property.Key);
            builder.Append('=');
            builder.Append(property.Value);
        }

        public static Parameters FromString(string data)
        {
            throw new NotImplementedException();
        }
    }
}
