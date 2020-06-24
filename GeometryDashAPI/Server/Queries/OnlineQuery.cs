namespace GeometryDashAPI.Server.Queries
{
    public class OnlineQuery : IQuery
    {
        public int GameVersion { get; set; } = 21;
        public int BinaryVersion { get; set; } = 35;
        public int GDW { get; set; } = 0;
        public string Secret { get; set; } = "Wmfd2893gb7";

        public virtual Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(new Property("gameVersion", GameVersion));
            parameters.Add(new Property("binaryVersion", BinaryVersion));
            parameters.Add(new Property("gdw", GDW));
            parameters.Add(new Property("secret", Secret));
        }
    }
}
