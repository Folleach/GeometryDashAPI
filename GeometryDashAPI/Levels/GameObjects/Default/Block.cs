using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    [GameBlock()]
    public class Block : GameObject, IBlock
    {
        [GameProperty("1", 0, true)] public int Id { get; set; }
        [GameProperty("2", 0, true)] public float PositionX { get; set; }
        [GameProperty("3", 0, true)] public float PositionY { get; set; }
        [GameProperty("4", false)] public bool HorizontalReflection { get; set; }
        [GameProperty("5", false)] public bool VerticalReflection { get; set; }
        [GameProperty("6", (int)0)] public int Rotation { get; set; }
        public bool Glow
        {
            get => !glow;
            set => glow = !value;
        }
        [GameProperty("96", true)] private bool glow = true;
        [GameProperty("108", 0)] public int LinkControl { get; set; }
        [GameProperty("20", (short)0)] public short EditorL { get; set; }
        [GameProperty("61", (short)0)] public short EditorL2 { get; set; }
        [GameProperty("103", false)] public bool HighDetail { get; set; }
        [GameProperty("57", null)] public BlockGroup Group { get; set; }
        [GameProperty("64", false)] public bool DontFade { get; set; }
        [GameProperty("67", false)] public bool DontEnter { get; set; }
        [GameProperty("25", 2)] public virtual int ZOrder { get; set; } = 2;
        public Layer ZLayer
        {
            get => (Layer)zLayer;
            set => zLayer = (short)value;
        }
        [GameProperty("24", (short)Layer.T1)] protected virtual short zLayer { get; set; } = (int)Layer.T1;
        [GameProperty("32", 1f)] public float Scale { get; set; } = 1f;
        [GameProperty("34", false)] public bool GroupParent { get; set; }
        [GameProperty("36", false)] public bool IsTrigger { get; set; }

        public Block()
        {
        }
        
        public Block(int id)
        {
            Id = id;
        }
        
        public override string GetParserSense() => ",";
    }
}
