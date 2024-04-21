using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

public enum SoundPropagationDirection
{
    All,
    Horizontal,
    Left,
    Right,
    Vertical,
    Down,
    Up
}

public enum ReverbTypes
{
    Generic,
    PaddedCell,
    Room,
    BathRoom,
    LivingRoom,
    StoneRoom,
    Auditorium,
    ConcertHall,
    Cave,
    Arena,
    Hangar,
    CarpetedHallway,
    Hallway,
    StoneCorridor,
    Alley,
    Forest,
    City,
    Mountains,
    Quarry,
    Plain,
    ParkingLot,
    SewerPipe,
    Underwater
}

[GameBlock(3602)]
public class SfxTrigger : Trigger
{
    [GameProperty("51", 0)]
    public int GroupId1 { get; set; }

    [GameProperty("71", 0)]
    public int GroupId2 { get; set; }

    [GameProperty("138", false)]
    public bool P1 { get; set; }

    [GameProperty("200", false)]
    public bool P2 { get; set; }

    [GameProperty("392", 0)]
    public int SongId { get; set; }

    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("405", 0)]
    public int Pitch { get; set; }

    [GameProperty("406", 1f, alwaysSet: true)]
    public float Volume { get; set; } = 1f;

    [GameProperty("407", false)]
    public bool Reverb { get; set; }

    [GameProperty("408", 0)]
    public int Start { get; set; }

    [GameProperty("409", 0)]
    public int FadeIn { get; set; }

    [GameProperty("410", 0)]
    public int End { get; set; }
    
    [GameProperty("411", 0)]
    public int FadeOut { get; set; }

    [GameProperty("412", false)]
    public bool Fft { get; set; }

    [GameProperty("413", false)]
    public bool Loop { get; set; }

    [GameProperty("415", false)]
    public bool IsUnique { get; set; }

    [GameProperty("416", 0)]
    public int UniqueId { get; set; }

    [GameProperty("420", false)]
    public bool Override { get; set; }

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

    [GameProperty("433", false)]
    public bool PreLoad { get; set; }

    [GameProperty("434", 0f)]
    public float MinInterval { get; set; }

    [GameProperty("455", 0)]
    public int SfxGroup { get; set; }

    [GameProperty("458", SoundPropagationDirection.All)]
    public SoundPropagationDirection Propagation { get; set; }

    [GameProperty("489", false)]
    public bool IgnoreVolumeTest { get; set; }

    [GameProperty("490", false)]
    public float Duration { get; set; }

    [GameProperty("502", ReverbTypes.Generic)]
    public ReverbTypes ReverbType { get; set; }

    [GameProperty("503", false)]
    public bool EnableReverb { get; set; }

    public SfxTrigger() : base(3602)
    {
    }
}
