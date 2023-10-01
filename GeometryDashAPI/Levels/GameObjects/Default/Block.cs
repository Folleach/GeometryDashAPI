using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    [GameBlock()]
    [Sense(",")]
    public class Block : GameObject, IBlock
    {
        [GameProperty("1", 0, true, Order = 1)] public int Id { get; set; }
        [GameProperty("2", 0f, true, Order = 2)] public float PositionX { get; set; }
        [GameProperty("3", 0f, true, Order = 3)] public float PositionY { get; set; }

        [GameProperty("4", false, Order = 4)] public bool HorizontalReflection { get; set; }
        [GameProperty("5", false, Order = 5)] public bool VerticalReflection { get; set; }

        [GameProperty("64", false, Order = 50)] public bool DontFade { get; set; }

        [GameProperty("36", false, Order = Trigger.OrderTriggerBase)] public bool IsTrigger { get; set; }

        [GameProperty("6", 0)] public int Rotation { get; set; }
        
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
        [GameProperty("57")] [ArraySeparator(".")] public int[] Groups { get; set; }
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

        public Block()
        {
        }

        public Block(int id)
        {
            Id = id;
        }

        public override string ToString() => $"Type: {GetType().Name}, ID: {Id}, Position: (x: {PositionX}, y: {PositionY})";
    }
}
