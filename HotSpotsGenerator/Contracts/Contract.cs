using HotSpotsGenerator.Employers;

namespace HotSpotsGenerator.Contracts;

public abstract class Contract
{
    public int Scale { get; set; }
    public int Length { get; set; }
    public Employer Employer { get; set; }
    public int BasePay { get; set; }
    public int CommandRights { get; set; }
    public int SalvageRights { get; set; }
    public int SupportRights { get; set; }
    public int TransportationTerms { get; set; }
    public virtual int BasePayModifier { get; }
    public virtual int CommandRightsModifier { get; }
    public virtual int SalvageRightsModifier { get; }
    public virtual int SupportRightsModifier { get; }
    public virtual int TransportationTermsModifier { get; }
    public TerrainType Terrain { get; set; }
    public Contract? OpposingContract { get; set; }
    public List<Track> Tracks { get; }

    public Contract()
    {
        Employer = new Self();
        Tracks = [];
    }

    public override string ToString() => GetType().Name;
}