using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers;

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
    [GameProperty("404", 0)]
    public int Speed { get; set; }

    [GameProperty("405", 0)]
    public int Pitch { get; set; }

    [GameProperty("406", 1f, alwaysSet: true)]
    public float Volume { get; set; } = 1f;
    
    [GameProperty("455", 0)]
    public int SfxGroup { get; set; }
    
    [GameProperty("408", 0)]
    public int Start { get; set; }
    
    [GameProperty("410", 0)]
    public int End { get; set; }
    
    [GameProperty("409", 0)]
    public int FadeIn { get; set; }
    
    [GameProperty("411", 0)]
    public int FadeOut { get; set; }

    [GameProperty("407", false)]
    public bool Reverb { get; set; }

    [GameProperty("412", false)]
    public bool Fft { get; set; }

    [GameProperty("413", false)]
    public bool Loop { get; set; }

    [GameProperty("433", false)]
    public bool PreLoad { get; set; }

    [GameProperty("392", false)]
    public int SongId { get; set; }

    [GameProperty("490", false)]
    public float Duration { get; set; }

    [GameProperty("502", false)]
    public ReverbTypes ReverbType { get; set; }

    [GameProperty("503", false)]
    public bool EnableReverb { get; set; }

    public SfxTrigger() : base(3602)
    {
    }
}
