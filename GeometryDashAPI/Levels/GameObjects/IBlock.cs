using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects
{
    public interface IBlock
    {
        int Id { get; set; }
        float PositionX { get; set; }
        float PositionY { get; set; }
        bool HorizontalReflection { get; set; }
        bool VerticalReflection { get; set; }
        int Rotation { get; set; }
        bool Glow { get; set; }
        int LinkControl { get; set; }
        short EditorL { get; set; }
        short EditorL2 { get; set; }
        bool HighDetail { get; set; }
        int[] Groups { get; set; }
        bool DontFade { get; set; }
        bool DontEnter { get; set; }
        int ZOrder { get; set; }
        Layer ZLayer { get; set; }
        float Scale { get; set; }
        bool GroupParent { get; set; }
        bool IsTrigger { get; set; }
    }
}