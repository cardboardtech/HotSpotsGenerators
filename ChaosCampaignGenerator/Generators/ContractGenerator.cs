using ChaosCampaignGenerator.Contracts;
using ChaosCampaignGenerator.Employers;

namespace ChaosCampaignGenerator.Generators;

public class ContractGenerator
{
    private readonly Dice _dice;

    public ContractGenerator(Dice dice)
    {
        _dice = dice;
    }

    public Contract GenerateContract(int numberOfTerms = 1)
    {
        Contract contract = GetContractType();

        for (int i = 0; i < numberOfTerms; i++)
        {
            PopulateContract(contract);
        }

        contract.Terrain = GetTerrain();

        return contract;
    }

    public Contract GenerateOpposingContract(Contract primaryContract, int numberOfTerms = 1)
    {
        Contract contract = GetOpposingContractType(primaryContract);

        for (int i = 0; i < numberOfTerms; i++)
        {
            PopulateContract(contract);
        }

        contract.Length = primaryContract.Length;
        contract.Terrain = primaryContract.Terrain;

        return contract;
    }

    private void PopulateContract(Contract contract)
    {
        var terms = new ContractTerms();

        Employer employer = GetEmployer();

        terms.Employer = employer;
        terms.BasePay = GetBasePay(contract.BasePayModifier, employer.BasePayModifier);
        terms.CommandRights = GetCommandRights(contract.CommandRightsModifier, employer.CommandRightsModifier);
        terms.SalvageRights = GetSalvageRights(contract.SalvageRightsModifier, employer.SalvageRightsModifier);
        terms.SupportRights = GetSupportRights(contract.SupportRightsModifier, employer.SupportRightsModifier);
        terms.TransportationTerms = GetTransportationTerms(contract.TransportationTermsModifier, employer.TransportationTermsModifier);

        contract.Terms.Add(terms);
    }

    private Contract GetContractType()
    {
        Contract contractType = _dice.Roll(2) switch
        {
            <= 4 => GetExpeditionType(),
            >= 5 and <= 6 => GetGarrisonType(),
            >= 7 and <= 9 => new Raid(),
            >= 10 => new Invasion()
        };
        return contractType;

        //local functions
        Contract GetExpeditionType()
        {
            return _dice.Roll(2) switch
            {
                <= 8 => new Expedition(),
                >= 9 and <= 11 => new PirateHunt(),
                >= 12 => new GuerillaOperation()
            };
        }

        Contract GetGarrisonType()
        {
            return _dice.Roll(2) switch
            {
                <= 5 => new CadreDuty(),
                >= 6 => new Garrison()
            };
        }
    }

    private Contract GetOpposingContractType(Contract primaryContract)
    {
        Contract opposingContractType = primaryContract switch
        {
            Expedition => GetExpeditionOpposition(),
            Garrison => GetGarrisonOpposition(),
            Raid => GetRaidOpposition(),
            Invasion => GetInvasionOpposition(),
            _ => throw new Exception()
        };
        return opposingContractType;

        //Local Functions
        Contract GetExpeditionOpposition()
        {
            return _dice.Roll(2) switch
            {
                <= 8 => new Garrison(),
                >= 9 => new Raid()
            };
        }

        Contract GetGarrisonOpposition()
        {
            return _dice.Roll(2) switch
            {
                <= 4 => new Expedition(),
                >= 5 and <= 8 => new Raid(),
                >= 9 => new Invasion()
            };
        }

        Contract GetRaidOpposition()
        {
            return _dice.Roll(2) switch
            {
                <= 7 => new Expedition(),
                >= 8 and <= 10 => new Garrison(),
                11 => new Raid(),
                >= 12 => new Invasion()
            };
        }

        Contract GetInvasionOpposition()
        {
            return _dice.Roll(2) switch
            {
                <= 4 => new Expedition(),
                >= 5 and <= 8 => new Garrison(),
                9 => new Raid(),
                >= 10 => new Invasion()
            };
        }
    }

    private Employer GetEmployer()
    {
        Employer employer = _dice.Roll(2) switch
        {
            2 => new CivilianOrganization(),
            3 or 10 => new PlanetaryGovernment(),
            4 or 12 => new MercenarySubcontract(),
            5 or 9 => new Corporation(),
            6 or 7 or 11 => new HouseGovernment(),
            8 => new Noble(),
            _ => throw new Exception()
        };
        return employer;
    }

    private int GetBasePay(int contractModfier, int employerModifier)
    {
        int payRateStep = _dice.Roll(2) switch
        {
            <= 3 => 3,
            >= 4 and <= 5 => 4,
            >= 6 and <= 7 => 5,
            >= 8 and <= 9 => 6,
            >= 10 and <= 11 => 7,
            >= 12 => 8
        };

        return payRateStep + contractModfier + employerModifier;
    }

    private int GetCommandRights(int contractModifier, int employerModifier)
    {
        int commandRightsStep = _dice.Roll(2) switch
        {
            <= 5 => 3,
            >= 6 and <= 7 => 7,
            >= 8 and <= 9 => 8,
            >= 10 => 11
        };
        int adjustedStep = commandRightsStep + contractModifier + employerModifier;

        return adjustedStep switch
        {
            <= 3 => 3,
            >= 4 and <= 7 => 7,
            >= 8 and <= 11 => 8,
            >= 11 => 11
        };
    }

    private int GetSalvageRights(int contractModfier, int employerModifier)
    {
        int salvageRightsStep = _dice.Roll(2) switch
        {
            <= 5 => 3,
            >= 6 and <= 7 => 4,
            >= 8 and <= 9 => 5,
            >= 10 and <= 11 => 6,
            >= 12 => 7
        };
        int adjustedStep = salvageRightsStep + contractModfier + employerModifier;

        return adjustedStep switch
        {
            < 1 => 1,
            2 => 3,
            _ => adjustedStep
        };
    }

    private int GetSupportRights(int contractModfier, int employerModifier)
    {
        int supportRightsStep = _dice.Roll(2) switch
        {
            <= 5 => 3,
            >= 6 and <= 7 => 4,
            >= 8 and <= 9 => 5,
            >= 10 and <= 11 => 6,
            >= 12 => 7
        };
        int adjustedStep = supportRightsStep + contractModfier + employerModifier;

        return adjustedStep;
    }

    private int GetTransportationTerms(int contractModfier, int employerModifier)
    {
        int transportationTermsStep = _dice.Roll(2) switch
        {
            <= 5 => 5,
            >= 6 and <= 7 => 6,
            >= 8 and <= 9 => 7,
            >= 10 and <= 11 => 8,
            >= 12 => 9
        };
        int adjustedStep = transportationTermsStep + contractModfier + employerModifier;

        return adjustedStep switch
        {
            < 5 => 5,
            > 9 => 9,
            _ => adjustedStep
        };
    }

    private TerrainType GetTerrain()
    {
        TerrainType terrain = _dice.Roll(2) switch
        {
            2 => TerrainType.Desert,
            3 => TerrainType.Wetlands,
            4 => TerrainType.LightUrban,
            5 => TerrainType.Hills,
            6 => TerrainType.Wooded,
            7 => TerrainType.Grasslands,
            8 => TerrainType.Savannahs,
            9 or 11 => TerrainType.Urban,
            10 => TerrainType.Mountains,
            12 => TerrainType.Alien,
            _ => throw new Exception()
        };
        return terrain;
    }
}
