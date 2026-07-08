namespace HotSpotsGenerator.Generators;

public class PilotGenerator(Dice dice)
{
    private readonly Dice _dice = dice;
    private static readonly Dictionary<ExperienceLevel, Dictionary<int, int>> _numAbilitiesTable = new()
    {
        { ExperienceLevel.Regular, new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 }, { 4, 1 }, { 5, 2 }, { 6, 2 } } },
        { ExperienceLevel.Veteran, new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 2 }, { 5, 2 }, { 6, 2 } } },
        { ExperienceLevel.Elite, new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 2 }, { 5, 3 }, { 6, 3 } } },
        { ExperienceLevel.Heroic, new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 2 }, { 4, 2 }, { 5, 3 }, { 6, 3 } } },
        { ExperienceLevel.Legendary, new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 3 }, { 5, 4 }, { 6, 4 } } }
    };

    public Pilot GenerateGladiator(GladiatorClass gladiatorClass)
    {
        var gladiator = new Pilot
        {
            UnitClass = gladiatorClass switch
            {
                GladiatorClass.Light => UnitClass.Light,
                GladiatorClass.Medium => UnitClass.Medium,
                GladiatorClass.Heavy => UnitClass.Heavy,
                GladiatorClass.Assault => UnitClass.Assault,
                GladiatorClass.Open => UnitClass.Assault
            }
        };

        int experienceModifier = gladiatorClass switch
        {
            GladiatorClass.Light => -2,
            GladiatorClass.Medium => 0,
            GladiatorClass.Heavy => 2,
            GladiatorClass.Assault => 4,
            GladiatorClass.Open => 6
        };

        PopulatePilot(gladiator, experienceModifier);
        return gladiator;
    }

    public Pilot GenerateTemporaryHire()
    {
        var tempHire = new Pilot
        {
            UnitClass = _dice.Roll(2) switch
            {
                <= 5 => UnitClass.Light,
                >= 6 and <= 7 => UnitClass.Medium,
                >= 8 and <= 10 => UnitClass.Heavy,
                >= 11 => UnitClass.Assault
            }
        };

        PopulatePilot(tempHire);
        return tempHire;
    }

    private void PopulatePilot(Pilot pilot, int experienceModifier = 0)
    {
        pilot.ExperienceLevel = GetExperienceLevel(experienceModifier);
        SetSkillLevel(pilot);
        AssignEdgeAbilities(pilot);
        pilot.Faction = GetFaction();
    }

    private void AssignEdgeAbilities(Pilot pilot)
    {
        int roll = _dice.Roll();
        int numAbilities = _numAbilitiesTable[pilot.ExperienceLevel][roll];

        for (int i = 0; i < numAbilities; i++)
        {
            SetEdgeAbility();
        }

        //local function
        void SetEdgeAbility()
        {
            roll = _dice.Roll(2);

            if (roll == 7)
            {
                pilot.EdgeTokens++;
                return;
            }

            EdgeAbilities selectedAbility = roll switch
            {
                2 => EdgeAbilities.Assassin,
                3 => EdgeAbilities.CoolantFlush,
                4 => EdgeAbilities.Patient,
                5 => EdgeAbilities.JumpingJack,
                6 => EdgeAbilities.Cautious,
                8 => EdgeAbilities.Marksman,
                9 => EdgeAbilities.SpeedDemon,
                10 => EdgeAbilities.MeleeSpecialist,
                11 => EdgeAbilities.Nimble,
                12 => EdgeAbilities.Bulwark
            };

            if (pilot.EdgeAbilities.Contains(selectedAbility))
            {
                SetEdgeAbility();
            }
            else
            {
                pilot.EdgeAbilities.Add(selectedAbility);
            }
        }
    }

    private ExperienceLevel GetExperienceLevel(int experienceModifier)
    {
        return _dice.Roll(2, modifier: experienceModifier) switch
        {
            <= 5 => ExperienceLevel.Regular,
            >= 6 and <= 8 => ExperienceLevel.Veteran,
            >= 9 and <= 12 => ExperienceLevel.Elite,
            >= 13 and <= 15 => ExperienceLevel.Heroic,
            >= 16 => ExperienceLevel.Legendary
        };
    }

    private Faction GetFaction()
    {
        return _dice.Roll(2) switch
        {
            <= 4 => Faction.RepublicOfTheSphere,
            5 or 6 => Faction.DraconisCombine,
            7 or 8 => Faction.Mercenary,
            >= 9 => Faction.FederatedSuns
        };
    }

    /// <summary>
    /// Base Gunnery and Piloting is 5/5. Experience Level substracts 1 from each skill.
    /// Then a 1d3 roll determines whether they stay at the Base Skill or improve Gunnery or Piloting.
    /// </summary>
    private void SetSkillLevel(Pilot pilot)
    {
        pilot.Gunnery = pilot.Piloting = 5 - (int)pilot.ExperienceLevel;

        int roll = _dice.Roll(maxValue: 3);
        if (roll == 2)
        {
            pilot.Piloting--;
        }
        else if (roll == 3)
        {
            pilot.Gunnery--;
        }
    }
}
