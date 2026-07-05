using HotSpotsGenerator.Contracts;

namespace HotSpotsGenerator.Generators;

public class TrackGenerator
{
    private readonly Dice _dice;

    public TrackGenerator(Dice dice)
    {
        _dice = dice;
    }

    public void GenerateTracks(Contract contract)
    {
        int intensity = GetIntensity();

        if (intensity == 0)
        {
            if (contract is Garrison
                && contract.OpposingContract != null)
            {
                GenerateTracks(contract.OpposingContract);
            }

            return;
        }

        int[] monthlyTracks = GetMonthlyTracks();
        for (int month = 1; month <= contract.Length; month++)
        {
            for (int i = 1; i <= monthlyTracks[month - 1]; i++)
            {
                Track track = contract switch
                {
                    Raid => CreateRaidTrack(),
                    Expedition => CreateExpeditionTrack(),
                    Garrison => CreateGarrisonTrack(),
                    Invasion => CreateInvasionTrack(),
                    _ => throw new Exception("Invalid Contract Type")
                };
                track.Month = month;

                if (i < monthlyTracks[month - 1])
                {
                    track.RepairTime = _dice.Roll(2) switch
                    {
                        <= 2 => RepairTime.Hasty,
                        >= 3 and <= 9 => RepairTime.Standard,
                        >= 10 and <= 11 => RepairTime.Extended,
                        >= 12 => RepairTime.Full
                    };
                }

                track.Mapsheets.AddRange(SetMapsheets());
                contract.Tracks.Add(track);

                //generate alternate Raid tracks for Pirate Hunts
                if (contract is PirateHunt pirateHunt && contract.Tracks.Count > 1)
                {
                    Track raidTrack = CreateRaidTrack();
                    raidTrack.Month = track.Month;
                    raidTrack.RepairTime = track.RepairTime;
                    raidTrack.Mapsheets.AddRange(SetMapsheets());

                    pirateHunt.RaidTracks.Add(raidTrack);
                }
            }
        }

        //Local Functions
        int GetIntensity()
        {
            int roll = _dice.Roll(2);
            return contract switch
            {
                Raid or Expedition => roll switch
                {
                    <= 8 => 1,
                    >= 9 and <= 11 => 2,
                    >= 12 => 3
                },
                Garrison => roll switch
                {
                    <= 4 => 0,
                    >= 5 and <= 6 => 1,
                    >= 7 and <= 8 => 2,
                    >= 9 and <= 10 => 3,
                    11 => 4,
                    >= 12 => 5
                },
                Invasion => roll switch
                {
                    <= 5 => 2,
                    >= 6 and <= 7 => 3,
                    >= 8 and <= 9 => 4,
                    >= 10 and <= 11 => 5,
                    >= 12 => 6
                },
                _ => 0,
            };
        }

        int[] GetMonthlyTracks()
        {
            int roll = _dice.Roll();

            if (contract.Length == 3)
            {
                switch (intensity)
                {
                    case 1:
                        return roll switch
                        {
                            <= 3 => [0, 1, 0],
                            4 or 5 => [0, 0, 1],
                            >= 6 => [1, 0, 0]
                        };
                    case 2:
                        return roll switch
                        {
                            <= 3 => [0, 1, 1],
                            4 => [1, 0, 1],
                            5 => [0, 2, 0],
                            >= 6 => [0, 2, 0]
                        };
                    case 3:
                        return roll switch
                        {
                            <= 1 => [0, 1, 2],
                            2 or 3 => [1, 1, 1],
                            4 => [0, 2, 1],
                            5 => [2, 0, 1],
                            >= 6 => [2, 1, 0]
                        };
                    default:
                        return [0, 0, 0];
                }
            }
            else if (contract.Length == 6)
            {
                switch (intensity)
                {
                    case 1:
                        return roll switch
                        {
                            <= 1 => [0, 0, 1, 0, 0, 0],
                            2 => [0, 0, 0, 0, 1, 0],
                            3 => [0, 1, 0, 0, 0, 0],
                            4 => [1, 0, 0, 0, 0, 0],
                            5 => [0, 0, 0, 0, 0, 1],
                            >= 6 => [0, 0, 0, 1, 0, 0]
                        };
                    case 2:
                        return roll switch
                        {
                            <= 1 => [0, 1, 0, 1, 0, 0],
                            2 => [0, 0, 1, 0, 1, 0],
                            3 => [0, 0, 1, 0, 0, 1],
                            4 => [0, 1, 1, 0, 0, 0],
                            5 => [0, 0, 0, 1, 1, 0],
                            >= 6 => [0, 0, 2, 0, 0, 0]
                        };
                    case 3:
                        return roll switch
                        {
                            <= 1 => [0, 1, 0, 1, 0, 1],
                            2 => [0, 1, 0, 1, 1, 0],
                            3 => [0, 1, 1, 1, 0, 0],
                            4 => [0, 0, 0, 1, 1, 1],
                            5 => [0, 2, 0, 1, 0, 0],
                            >= 6 => [0, 0, 0, 2, 1, 0]
                        };
                    case 4:
                        return roll switch
                        {
                            <= 1 => [0, 1, 0, 1, 1, 1],
                            2 => [0, 1, 1, 1, 1, 0],
                            3 => [0, 0, 1, 1, 1, 1],
                            4 => [0, 2, 0, 1, 0, 1],
                            5 => [0, 0, 2, 0, 1, 1],
                            >= 6 => [0, 2, 1, 0, 1, 0]
                        };
                    case 5:
                        return roll switch
                        {
                            <= 1 => [0, 1, 1, 1, 1, 1],
                            2 => [0, 1, 1, 1, 0, 2],
                            3 => [0, 2, 0, 2, 0, 1],
                            4 => [0, 2, 1, 0, 1, 1],
                            5 => [0, 2, 2, 1, 0, 0],
                            >= 6 => [0, 0, 0, 2, 2, 1]
                        };
                    case 6:
                        return roll switch
                        {
                            <= 1 => [1, 1, 1, 1, 1, 1],
                            2 => [0, 2, 1, 1, 1, 1],
                            3 => [0, 0, 2, 1, 1, 2],
                            4 => [0, 1, 1, 2, 1, 1],
                            5 => [0, 1, 1, 2, 0, 2],
                            >= 6 => [0, 1, 2, 2, 1, 0]
                        };
                    default:
                        return [0, 0, 0, 0, 0, 0];
                }
            }
            else
            {
                throw new ArgumentException($"Only supports Contract Lengths of 3 or 6 months. Input Length was {contract.Length}");
            }
        }

        IEnumerable<int> SetMapsheets()
        {
            List<int> maps = [1, 2, 3, 4, 5, 6];
            List<int> results = [];

            int numberOfMaps = contract.Scale > 2 ? 4 : contract.Scale;
            var random = new Random();
            for (int i = 0; i < numberOfMaps; i++)
            {
                int sheet = random.Next(maps.Count - 1);
                results.Add(maps[sheet]);
                maps.RemoveAt(sheet);
            }

            return results;
        }
    }

    private Track CreateRaidTrack()
    {
        int attackerRoll = _dice.Roll();
        var track = new Track
        {
            TrackType = _dice.Roll() switch
            {
                1 => TrackType.Pushback,
                2 => TrackType.Breakthrough,
                3 => TrackType.Recon,
                4 => TrackType.Strike,
                5 or 6 => TrackType.ObjectiveRaid,
                _ => throw new Exception()
            },
            Attacker = attackerRoll >= 2 ? AttackerDefender.Primary : AttackerDefender.Opposition,
            Defender = attackerRoll >= 2 ? AttackerDefender.Opposition : AttackerDefender.Primary
        };
        return track;
    }

    private Track CreateExpeditionTrack()
    {
        var track = new Track
        {
            TrackType = _dice.Roll() switch
            {
                1 or 2 => TrackType.Recon,
                3 => TrackType.Pursuit,
                4 => TrackType.Flank,
                5 => TrackType.Strike,
                6 => TrackType.Retreat,
                _ => throw new Exception()
            }
        };

        track.Attacker = track.TrackType is TrackType.Recon or TrackType.Strike ? AttackerDefender.Primary : AttackerDefender.Opposition;
        track.Defender = track.Attacker == AttackerDefender.Primary ? AttackerDefender.Opposition : AttackerDefender.Primary;

        return track;
    }

    private Track CreateGarrisonTrack()
    {
        int attackerRoll = _dice.Roll();
        var track = new Track
        {
            TrackType = _dice.Roll() switch
            {
                1 or 6 => TrackType.Pursuit,
                2 => TrackType.MeetingEngagement,
                3 => TrackType.Recon,
                4 => TrackType.Pushback,
                5 => TrackType.Strike,
                _ => throw new Exception()
            },
            Attacker = attackerRoll >= 2 ? AttackerDefender.Opposition : AttackerDefender.Primary,
            Defender = attackerRoll >= 2 ? AttackerDefender.Primary : AttackerDefender.Opposition
        };
        return track;
    }

    private Track CreateInvasionTrack()
    {
        int attackerRoll = _dice.Roll();
        var track = new Track
        {
            TrackType = _dice.Roll() switch
            {
                1 => TrackType.Assault,
                2 => TrackType.Breakthrough,
                3 => TrackType.Flank,
                4 or 5 => TrackType.MeetingEngagement,
                6 => TrackType.Pushback,
                _ => throw new Exception()
            },
            Attacker = attackerRoll >= 3 ? AttackerDefender.Primary : AttackerDefender.Opposition,
            Defender = attackerRoll >= 3 ? AttackerDefender.Opposition : AttackerDefender.Primary
        };
        return track;
    }
}
