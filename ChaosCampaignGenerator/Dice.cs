namespace ChaosCampaignGenerator;

public class Dice
{
    private readonly Random _random;

    public Dice()
    {
        _random = new Random();
    }

    public int Roll(int numberOfDice = 1, int maxValue = 6, int modifier = 0)
    {
        int result = 0;
        for (int i = 0; i < numberOfDice; i++)
        {
            result += _random.Next(1, maxValue);
        }

        return result + modifier;
    }
}