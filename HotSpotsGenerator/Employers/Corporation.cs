namespace HotSpotsGenerator.Employers;

public class Corporation : Employer
{
    public override string Name { get; set; } = "Corporation";
    public override int BasePayModifier => 2;
    public override int SalvageRightsModifier => 2;
    public override int SupportRightsModifier => -2;
    public override int TransportationTermsModifier => 1;
}
