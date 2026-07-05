namespace HotSpotsGenerator.Employers;

public class CivilianOrganization : Employer
{
    public override string Name { get; set; } = "Civilian Organization";
    public override int BasePayModifier => -2;
    public override int CommandRightsModifier => 4;
    public override int SalvageRightsModifier => 4;
    public override int SupportRightsModifier => -2;
    public override int TransportationTermsModifier => -1;
}
