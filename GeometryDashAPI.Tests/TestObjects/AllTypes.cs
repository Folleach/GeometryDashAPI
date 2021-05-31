namespace GeometryDashAPI.Tests.TestObjects
{
    public class AllTypes : GameObject
    {
        [GameProperty("1")]
        public string String { get; set; }
        [GameProperty("2")]
        public byte Byte { get; set; }
        [GameProperty("3")]
        public short Short { get; set; }
        [GameProperty("4")]
        public int Int { get; set; }
        [GameProperty("5")]
        public long Long { get; set; }
        [GameProperty("6")]
        public double Double { get; set; }
        [GameProperty("7")]
        public bool Bool { get; set; }

        internal override string ParserSense { get; } = ",";
    }
}