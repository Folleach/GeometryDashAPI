using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

[GameBlock(1934)]
public class SongTrigger : Trigger
{
    [GameProperty("392", 0)]
    public int SongId { get; set; }

    [GameProperty("399", false)]
    public bool Prep { get; set; }

    [GameProperty("400", false)]
    public bool LoadPrep { get; set; }

    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("406", 1f, alwaysSet: true)]
    public float Volume { get; set; } = 1f;

    [GameProperty("408", 0)]
    public int Start { get; set; }

    [GameProperty("409", 0)]
    public int FadeIn { get; set; }

    [GameProperty("410", 0)]
    public int End { get; set; }

    [GameProperty("411", 0)]
    public int FadeOut { get; set; }

    [GameProperty("413", false)]
    public bool Loop { get; set; }

    [GameProperty("432", 0)]
    public int Channel { get; set; }

    public SongTrigger() : base(1934)
    {
    }
}
