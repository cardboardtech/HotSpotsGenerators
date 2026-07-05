namespace HotSpotsGenerator.Contracts;

public class PirateHunt : Expedition
{
    public List<Track> RaidTracks { get; }

    public PirateHunt()
    {
        RaidTracks = [];
    }

    public override string ToString() => "Pirate Hunt";
}
