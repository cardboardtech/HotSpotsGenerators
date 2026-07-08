using HotSpotsGenerator;
using HotSpotsGenerator.Contracts;
using HotSpotsGenerator.Generators;

var contractGenerator = new ContractGenerator(new Dice());
var trackGenerator = new TrackGenerator(new Dice());
var pilotGenerator = new PilotGenerator(new Dice());

while (true)
{
    int option;

    Console.WriteLine("Generate Contract(1) or Pilot(2): ");
    while (!int.TryParse(Console.ReadLine(), out option) || option <= 0 || option >= 3)
    {
        Console.Write("Enter 1 for Contract or 2 for Pilot: ");
    }

    if (option == 1)
    {
        GenerateContracts(contractGenerator, trackGenerator);
    }
    else if (option == 2)
    {
        GeneratePilot(pilotGenerator);
    }

    Console.WriteLine("Generate more Contracts or Pilots? (Y/N): ");
    if (string.Compare(Console.ReadLine(), "Y", true) != 0)
    {
        return;
    }
}

static void GenerateContracts(ContractGenerator contractGenerator, TrackGenerator trackGenerator)
{
    int numberOfContracts;
    int scale;
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

    ContractWriter.WriteToConsole(contracts);
}

static void GeneratePilot(PilotGenerator pilotGenerator)
{
    int option;
    Console.WriteLine("Generate a Temporary Hire(1) or a Gladiator(2): ");
    while (!int.TryParse(Console.ReadLine(), out option) || option <= 0 || option >= 3)
    {
        Console.Write("Enter 1 for Temporary Hire or 2 for Gladiator: ");
    }

    Pilot pilot;
    if (option == 2)
    {
        Console.WriteLine("Set Gladiator Class (Light(1), Medium(2), Heavy(3), Assault(4), Open(5)): ");
        while (!int.TryParse(Console.ReadLine(), out option) || option <= 0 || option >= 6)
        {
            Console.Write("Invalid input. Set Gladiator Class (Light(1), Medium(2), Heavy(3), Assault(4), Open(5)): ");
        }

        pilot = pilotGenerator.GenerateGladiator((GladiatorClass)option);
    }
    else
    {
        pilot = pilotGenerator.GenerateTemporaryHire();
    }

    PilotWriter.WriteToConsole(pilot);
}