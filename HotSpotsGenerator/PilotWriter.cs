using System.Text.RegularExpressions;

namespace HotSpotsGenerator;

public static class PilotWriter
{
    private const string pilotString =
@"Pilot
Experience Level: {0}
Skills (G/P): {1}/{2}
# Edge Tokens: {3}
Edge Abilities: {4}

Faction: {5}
Unit Class: {6}
";

    public static void WriteToConsole(Pilot pilot)
    {
        string printString = string.Format(
            pilotString,
            pilot.ExperienceLevel,
            pilot.Gunnery,
            pilot.Piloting,
            pilot.EdgeTokens,
            string.Join(", ", pilot.EdgeAbilities.Order().Select(x => AddSpacesToStrings(x.ToString()))),
            AddSpacesToStrings(pilot.Faction.ToString()),
            pilot.UnitClass);

        Console.WriteLine(printString);

        static string AddSpacesToStrings(string text)
        {
            return Regex.Replace(text, @"(?<!^)[A-Z]", " $&");
        }
    }
}
