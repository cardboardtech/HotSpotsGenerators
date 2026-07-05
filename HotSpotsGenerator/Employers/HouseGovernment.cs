namespace HotSpotsGenerator.Employers;

public class HouseGovernment : Employer
{
    public override string Name { get; set; } = "House Government";
    public override int BasePayModifier => 1;
    public override int CommandRightsModifier => -3;
    public override int SalvageRightsModifier => -2;
    public override int SupportRightsModifier => 2;
    public override int TransportationTermsModifier => 1;
}
