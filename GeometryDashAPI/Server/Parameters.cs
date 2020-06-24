using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server
{
    public class Parameters : List<Property>
    {
        public override string ToString()
        {
            StringBuilder requestContent = new StringBuilder();
            Enumerator enumerator = base.GetEnumerator();
            if (enumerator.MoveNext())
                AppendProperty(requestContent, enumerator.Current);
            while (enumerator.MoveNext())
            {
                requestContent.Append('&');
                AppendProperty(requestContent, enumerator.Current);
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
