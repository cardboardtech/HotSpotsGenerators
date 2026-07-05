namespace HotSpotsGenerator.Contracts;

public class Expedition : Contract
{
    public override int CommandRightsModifier => 2;
    public override int SupportRightsModifier => 1;

    public Expedition()
    {
        Length = 6;
    }
}
