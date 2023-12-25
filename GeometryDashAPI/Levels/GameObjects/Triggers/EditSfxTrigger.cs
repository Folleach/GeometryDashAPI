using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

[GameBlock(3603)]
public class EditSfxTrigger : Trigger
{
    [GameProperty("10", 0.5f)]
    public float Duration { get; set; } = 0.5f;

    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("406", 1f, alwaysSet: true)]
    public float Volume { get; set; } = 1f;

    [GameProperty("414", false)]
    public bool StopLoop { get; set; }

    [GameProperty("416", 0)]
    public int UniqueId { get; set; }

    [GameProperty("417", false)]
    public bool Stop { get; set; }

    [GameProperty("418", false)]
    public bool ChangeVolume { get; set; }

    [GameProperty("419", false)]
    public bool ChangeSpeed { get; set; }

    [GameProperty("455", 0)]
    public int SfxGroup { get; set; }

    [GameProperty("457", 0)]
    public int GroupId { get; set; }

    public EditSfxTrigger() : base(3603)
    {
    }
}
