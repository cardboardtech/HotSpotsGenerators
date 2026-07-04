using ChaosCampaignGenerator.Contracts;

namespace ChaosCampaignGenerator;

public class ContractWriter
{
    private const string _trackFormat = "{0, -5} {1,-18} {2, -12} {3, -12} {4}";

    public void WriteToConsole(List<Contract> contracts)
    {
        int contractNumber = 1;
        foreach (Contract contract in contracts)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"Primary Contract {contractNumber} - {contract}");

            WriteContract(contract);

            if (contract.OpposingContract != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Opposing Contract {contractNumber} - {contract.OpposingContract}");

                WriteContract(contract.OpposingContract);
            }

            WriteTracks(contract);

            Console.WriteLine();
            contractNumber++;
        }

        Console.WriteLine("--------------------");

        //Local Function
        void WriteContract(Contract contract)
        {
            string contractString = GetContractString(contract);
            Console.WriteLine(contractString);
            Console.WriteLine();

            var termsNumber = 1;
            foreach (var terms in contract.Terms)
            {
                Console.WriteLine($"Contract Terms #{termsNumber}");
                string contractTermsString = GetContractTermsString(terms);
                Console.WriteLine(contractTermsString);
                Console.WriteLine();

                termsNumber++;
            }
        }

        void WriteTracks(Contract contract)
        {
            if (contract.Tracks.Count > 0)
            {
                PirateHunt? pirateHunt = contract as PirateHunt;

                Console.WriteLine();
                Console.WriteLine(string.Format(_trackFormat, "Month", pirateHunt == null ? "Tracks" : "Expedition Tracks", "Attacker", "Defender", "Mapsheets"));
                foreach (Track track in contract.Tracks.OrderBy(x => x.Month))
                {
                    Console.WriteLine(GetTrackString(track, contract.Terrain));
                }

                if (pirateHunt != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Raid Tracks");
                    foreach (Track track in pirateHunt.RaidTracks)
                    {
                        Console.WriteLine(GetTrackString(track, contract.Terrain));
                    }
                }
            }
        }
    }

    private static string GetContractString(Contract contract)
    {
        string contractString =
$@"Type of Action: {contract}
Length of Contract: {contract.Length} months
Location (Primary Terrain): {contract.Terrain}
Intensity (Number of Tracks): {contract.Tracks.Count}";

        return contractString;
    }

    private static string GetContractTermsString(ContractTerms terms)
    {
        string contractString =
$@"Employer: {terms.Employer}
Base Pay: {Tables.ContractTermsTable.BasePaySteps[terms.BasePay]} ({terms.BasePay})
Support: {Tables.ContractTermsTable.SupportRightsSteps[terms.SupportRights]} ({terms.SupportRights})
Transportation: {Tables.ContractTermsTable.TransportationTermsSteps[terms.TransportationTerms]} ({terms.TransportationTerms})
Salvage Rights: {Tables.ContractTermsTable.SalvageRightsSteps[terms.SalvageRights]} ({terms.SalvageRights})
Command Rights: {Tables.ContractTermsTable.CommandRightsSteps[terms.CommandRights]} ({terms.CommandRights})";

        return contractString;
    }

    private static string GetTrackString(Track track, TerrainType terrain)
    {
        string trackString = string.Format(_trackFormat,
            track.Month,
            track.TrackType,
            track.Attacker,
            track.Defender,
            string.Join(" | ", track.Mapsheets.Select(x => Tables.TerrainTables.Terrain[terrain][x])));

        return trackString;
    }
}
