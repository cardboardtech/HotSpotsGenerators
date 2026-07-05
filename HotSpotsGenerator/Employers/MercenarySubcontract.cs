namespace HotSpotsGenerator.Employers;

public class MercenarySubcontract : Employer
{
    public override string Name { get; set; } = "Mercenary Subcontract";
    public override int BasePayModifier => -1;
    public override int CommandRightsModifier => 3;
}
