namespace HotSpotsGenerator.Contracts;

public class Invasion : Contract
{
    public override int BasePayModifier => -1;
    public override int CommandRightsModifier => -2;
    public override int SalvageRightsModifier => 1;
    public override int SupportRightsModifier => 2;
    public override int TransportationTermsModifier => -1;

    public Invasion()
    {
        Length = 6;
    }
}
