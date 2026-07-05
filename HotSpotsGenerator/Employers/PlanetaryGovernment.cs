namespace HotSpotsGenerator.Employers;

public class PlanetaryGovernment : Employer
{
    public override string Name { get; set; } = "Planetary Government";
    public override int SalvageRightsModifier => 1;
    public override int SupportRightsModifier => 1;
}
