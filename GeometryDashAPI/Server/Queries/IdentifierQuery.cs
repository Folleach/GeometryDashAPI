using System;

namespace GeometryDashAPI.Server.Queries
{
    public class IdentifierQuery : IQuery
    {
        public Guid UDID { get; set; } = Guid.Parse("00000000-ffff-dddd-5555-123456789abc");
        public int UUID { get; set; } = 1;

        public IdentifierQuery()
        {
        }

        public Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(new Property("udid", UDID));
            parameters.Add(new Property("uuid", UUID));
        }
    }
}
