namespace HotSpotsGenerator.Employers;

public abstract class Employer
{
    public abstract string Name { get; set; }
    public virtual int BasePayModifier { get; }
    public virtual int CommandRightsModifier { get; }
    public virtual int SalvageRightsModifier { get; }
    public virtual int SupportRightsModifier { get; }
    public virtual int TransportationTermsModifier { get; }

    public override string ToString()
    {
        return Name;
    }
}
