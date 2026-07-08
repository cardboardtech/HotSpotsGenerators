namespace HotSpotsGenerator;

public class Pilot
{
    public ExperienceLevel ExperienceLevel { get; set; }
    public int Gunnery { get; set; }
    public int Piloting { get; set; }
    public Faction Faction { get; set; }
    public int EdgeTokens { get; set; } = 1;
    public List<EdgeAbilities> EdgeAbilities { get; set; } = [];
    public UnitClass UnitClass { get; set; }
    public string Unit { get; set; } = string.Empty;
}
