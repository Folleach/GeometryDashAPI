using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects;

public interface IBlock : IGameObject
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

    bool HasHsv { get; }
    bool HasAdditionalHsv { get; }

    /// <summary>
    /// If block has 1 color: <b>base</b> or <b>detail</b><br/>
    /// The <see cref="Hsv"/> value takes either <b>base</b> or <b>detail</b><br/><br/>
    /// If block has 2 colors, <see cref="Hsv"/> is <b>base</b>
    /// </summary>
    Hsv? Hsv { get; set; }

    /// <summary>
    /// If block has 2 colors: <b>base</b> and <b>detail</b><br/>
    /// <see cref="AdditionalHsv"/> is detail, otherwise null
    /// </summary>
    Hsv? AdditionalHsv { get; set; }
}
