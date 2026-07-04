using ChaosCampaignGenerator.Employers;

namespace ChaosCampaignGenerator.Contracts;

public class ContractTerms
{
    public Employer Employer { get; set; }
    public int BasePay { get; set; }
    public int CommandRights { get; set; }
    public int SalvageRights { get; set; }
    public int SupportRights { get; set; }
    public int TransportationTerms { get; set; }

    public ContractTerms()
    {
        Employer = new Self();
    }
}
