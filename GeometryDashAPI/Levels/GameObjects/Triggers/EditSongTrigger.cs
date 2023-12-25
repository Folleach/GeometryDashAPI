using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

[GameBlock(3605)]
public class EditSongTrigger : Trigger
{
    [GameProperty("10", 0.5f)]
    public float Duration { get; set; } = 0.5f;
    
    [GameProperty("432", 0)]
    public int Channel { get; set; }
    
    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("406", 1f)]
    public float Volume { get; set; } = 1f;

    [GameProperty("414", false)]
    public bool StopLoop { get; set; }

    [GameProperty("417", false)]
    public bool Stop { get; set; }

    [GameProperty("418", false)]
    public bool ChangeVolume { get; set; }

    [GameProperty("419", false)]
    public bool ChangeSpeed { get; set; }

    public EditSongTrigger() : base(3605)
    {
    }
}
