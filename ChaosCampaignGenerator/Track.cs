namespace ChaosCampaignGenerator;

public class Track
{
    public TrackType TrackType { get; set; }
    public AttackerDefender Attacker { get; set; }
    public AttackerDefender Defender { get; set; }
    public int Month { get; set; }
    public RepairTime? RepairTime { get; set; }
    public List<int> Mapsheets { get; private set; }

    public Track()
    {
        Mapsheets = [];
    }
}
