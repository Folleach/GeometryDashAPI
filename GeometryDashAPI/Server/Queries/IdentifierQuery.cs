using System;

namespace GeometryDashAPI.Server.Queries
{
    public class IdentifierQuery : IQuery
    {
        private readonly Guid udid;
        private readonly int uuid;

        private readonly Property udidProperty;
        private readonly Property uuidProperty;

        public IdentifierQuery(Guid udid, int uuid)
        {
            this.udid = udid;
            this.uuid = uuid;

            udidProperty = new Property("udid", udid);
            uuidProperty = new Property("uuid", uuid);
        }

        public Parameters BuildQuery()
        {
            var result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(udidProperty);
            parameters.Add(uuidProperty);
        }

        public static IdentifierQuery Default => new(Guid.Parse("00000000-ffff-dddd-5555-123456789abc"), 1);
    }
}
