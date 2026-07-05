using ChaosCampaignGenerator;
using ChaosCampaignGenerator.Contracts;
using ChaosCampaignGenerator.Generators;

int numberOfContracts;
int scale;
var contractGenerator = new ContractGenerator(new Dice());
var trackGenerator = new TrackGenerator(new Dice());

while (true)
{
    Console.Write("Number of contracts to Generate: ");
    while (!int.TryParse(Console.ReadLine(), out numberOfContracts) || numberOfContracts <= 0)
    {
        Console.Write("Number of contracts must be a number greater than zero: ");
    }

    Console.Write("Enter scale of the contracts: ");
    while (!int.TryParse(Console.ReadLine(), out scale) || scale <= 0)
    {
        Console.Write("Scale must be a number greater than zero: ");
    }

    List<Contract> contracts = [];
    for (int i = 0; i < numberOfContracts; i++)
    {
        Contract contract = contractGenerator.GenerateContract(scale);
        Contract opposingContract = contractGenerator.GenerateOpposingContract(contract);
        contract.OpposingContract = opposingContract;
        trackGenerator.GenerateTracks(contract);
        opposingContract?.Tracks.AddRange(contract.Tracks);

        contracts.Add(contract);
    }

    var writer = new ContractWriter();
    writer.WriteToConsole(contracts);

    Console.WriteLine("Generate more contracts? (Y/N)");
    if (string.Compare(Console.ReadLine(), "Y", true) != 0)
    {
        return;
    }
}