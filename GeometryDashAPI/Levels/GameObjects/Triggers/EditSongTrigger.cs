using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

[GameBlock(3605)]
public class EditSongTrigger : Trigger
{
    [GameProperty("10", 0.5f)]
    public float Duration { get; set; } = 0.5f;

    [GameProperty("51", 0)]
    public int GroupId1 { get; set; }

    [GameProperty("71", 0)]
    public int GroupId2 { get; set; }

    [GameProperty("138", false)]
    public bool P1 { get; set; }

    [GameProperty("200", false)]
    public bool P2 { get; set; }

    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("406", 1f, alwaysSet: true)]
    public float Volume { get; set; } = 1f;

    [GameProperty("414", false)]
    public bool StopLoop { get; set; }

    [GameProperty("417", false)]
    public bool Stop { get; set; }

    [GameProperty("418", false)]
    public bool ChangeVolume { get; set; }

    [GameProperty("419", false)]
    public bool ChangeSpeed { get; set; }

    [GameProperty("421", 1f)]
    public float VolNear { get; set; } = 1f;

    [GameProperty("422", 0.5f)]
    public float VolMed { get; set; } = 0.5f;

    [GameProperty("423", 0f)]
    public float VolFar { get; set; }

    [GameProperty("424", 0f)]
    public int MinDist { get; set; }

    [GameProperty("425", 0f)]
    public int Dist2 { get; set; }

    [GameProperty("426", 0f)]
    public int Dist3 { get; set; }

    [GameProperty("428", false)]
    public bool Cam { get; set; }

    [GameProperty("432", 0)]
    public int Channel { get; set; }

    [GameProperty("458", SoundPropagationDirection.All)]
    public SoundPropagationDirection Propagation { get; set; }

    public EditSongTrigger() : base(3605)
    {
    }
}
