using HotSpotsGenerator.Contracts;

namespace HotSpotsGenerator;

public static class ContractWriter
{
    private const string _trackFormat = "{0, -5} {1,-18} {2, -12} {3, -12} {4, -11} {5}";

    public static void WriteToConsole(List<Contract> contracts)
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

            Console.WriteLine($"Contract Terms");
            string contractTermsString = GetContractTermsString(contract);
            Console.WriteLine(contractTermsString);
            Console.WriteLine();
        }

        void WriteTracks(Contract contract)
        {
            if (contract.Tracks.Count > 0)
            {
                PirateHunt? pirateHunt = contract as PirateHunt;

                Console.WriteLine(string.Format(_trackFormat, "Month", pirateHunt == null ? "Tracks" : "Expedition Tracks", "Attacker", "Defender", "Repair Time", "Mapsheets"));
                foreach (Track track in contract.Tracks)
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
            else if (contract.OpposingContract?.Tracks.Count > 0)
            {
                Console.WriteLine("Opposing Contract Tracks (Primary Contract has Intensity 0)");
                Console.WriteLine(string.Format(_trackFormat, "Month", "Tracks", "Attacker", "Defender", "Repair Time", "Mapsheets"));
                foreach (Track track in contract.OpposingContract.Tracks)
                {
                    Console.WriteLine(GetTrackString(track, contract.Terrain));
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
Intensity: {contract.Tracks.Count}";

        return contractString;
    }

    private static string GetContractTermsString(Contract contract)
    {
        string contractString =
$@"Employer: {contract.Employer}
Base Pay: {Tables.ContractTermsTable.BasePaySteps[contract.BasePay]} ({contract.BasePay})
Support: {Tables.ContractTermsTable.SupportRightsSteps[contract.SupportRights]} ({contract.SupportRights})
Transportation: {Tables.ContractTermsTable.TransportationTermsSteps[contract.TransportationTerms]} ({contract.TransportationTerms})
Salvage Rights: {Tables.ContractTermsTable.SalvageRightsSteps[contract.SalvageRights]} ({contract.SalvageRights})
Command Rights: {Tables.ContractTermsTable.CommandRightsSteps[contract.CommandRights]} ({contract.CommandRights})";

        return contractString;
    }

    private static string GetTrackString(Track track, TerrainType terrain)
    {
        string trackString = string.Format(_trackFormat,
            track.Month,
            track.TrackType,
            track.Attacker,
            track.Defender,
            track.RepairTime,
            string.Join(" | ", track.Mapsheets.Select(x => Tables.TerrainTables.Terrain[terrain][x])));

        return trackString;
    }
}
