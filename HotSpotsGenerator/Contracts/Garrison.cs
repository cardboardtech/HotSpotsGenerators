namespace HotSpotsGenerator.Contracts;

public class Garrison : Contract
{
    public override int BasePayModifier => 1;
    public override int SalvageRightsModifier => -2;
    public override int TransportationTermsModifier => 1;

    public Garrison()
    {
        Length = 6;
    }
}