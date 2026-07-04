namespace ChaosCampaignGenerator.Contracts;

public abstract class Contract
{
    public int Scale { get; set; } = 2;
    public int Length { get; set; }
    public virtual int BasePayModifier { get; }
    public virtual int CommandRightsModifier { get; }
    public virtual int SalvageRightsModifier { get; }
    public virtual int SupportRightsModifier { get; }
    public virtual int TransportationTermsModifier { get; }
    public List<ContractTerms> Terms { get; }
    public TerrainType Terrain { get; set; }
    public Contract? OpposingContract { get; set; }
    public List<Track> Tracks { get; }

    public Contract()
    {
        Terms = [];
        Tracks = [];
    }

    public override string ToString() => GetType().Name;
}