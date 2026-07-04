using ChaosCampaignGenerator;
using ChaosCampaignGenerator.Contracts;
using ChaosCampaignGenerator.Generators;

int input;
Console.Write("Number of Contracts to Generate: ");
while (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
{
    Console.Write("Please enter a number greater than zero: ");
}

var contractGenerator = new ContractGenerator(new Dice());
var trackGenerator = new TrackGenerator(new Dice());

List<Contract> contracts = [];
for (int i = 0; i < input; i++)
{
    Contract contract = contractGenerator.GenerateContract(2);
    Contract opposingContract = contractGenerator.GenerateOpposingContract(contract, 2);
    contract.OpposingContract = opposingContract;
    trackGenerator.GenerateTracks(contract);
    opposingContract?.Tracks.AddRange(contract.Tracks);

    contracts.Add(contract);
}

var writer = new ContractWriter();
writer.WriteToConsole(contracts);

Console.ReadLine();