using System.Text;

namespace Blackjack;

class Player
{
    public Hand hand { get; set; }

    public Player()
    {
        hand = new Hand();
    }

    public string MakeChoice(bool doubleDownAvailable)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("Choose action (Hit [");
        stringBuilder.AppendJoin(", ", Constants.HitChoices);
        stringBuilder.Append("] or Stand [");
        stringBuilder.AppendJoin(", ", Constants.StandChoices);

        if (doubleDownAvailable)
        {
            stringBuilder.Append("]");
            stringBuilder.Append(" or Double Down [");
            stringBuilder.AppendJoin(", ", Constants.DoubleDownChoices);
        }

        stringBuilder.Append("]):");

        Console.WriteLine(stringBuilder.ToString());

        string? choice = Console.ReadLine();
        string[] validChoices = Constants.HitChoices
            .Concat(Constants.StandChoices)
            .Concat(Constants.DoubleDownChoices)
            .ToArray();

        while (choice == null || !validChoices.Contains(choice))
        {
            choice = Console.ReadLine();
        }

        return choice;
    }
}
