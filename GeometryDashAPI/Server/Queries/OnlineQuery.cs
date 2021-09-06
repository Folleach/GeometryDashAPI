namespace GeometryDashAPI.Server.Queries
{
    public class OnlineQuery : IQuery
    {
        private readonly int gameVersion;
        private readonly int binaryVersion;
        private readonly int gdw;
        private readonly string secret;

        public OnlineQuery(int gameVersion, int binaryVersion, int gdw, string secret)
        {
            this.gameVersion = gameVersion;
            this.binaryVersion = binaryVersion;
            this.gdw = gdw;
            this.secret = secret;
        }
        
        public virtual Parameters BuildQuery()
        {
            var result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(new Property("gameVersion", gameVersion));
            parameters.Add(new Property("binaryVersion", binaryVersion));
            parameters.Add(new Property("gdw", gdw));
            parameters.Add(new Property("secret", secret));
        }
        
        public static OnlineQuery Default { get; } = new(21, 35, 0, "Wmfd2893gb7");
    }
}
