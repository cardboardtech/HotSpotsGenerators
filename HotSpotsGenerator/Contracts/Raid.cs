namespace HotSpotsGenerator.Contracts;

public class Raid : Contract
{
    public override int SalvageRightsModifier => -1;

    public Raid()
    {
        Length = 3;
    }
}
