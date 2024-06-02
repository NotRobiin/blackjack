using System.Text;

namespace Blackjack
{
    class Player
    {
        public Hand hand;

        public Player()
        {
            hand = new Hand();
        }

        public string MakeChoice()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Choose action (Hit [");
            stringBuilder.AppendJoin(", ", Constants.HitChoices);
            stringBuilder.Append("] or Stand [");
            stringBuilder.AppendJoin(", ", Constants.StandChoices);
            stringBuilder.Append("]):");

            Console.WriteLine(stringBuilder.ToString());

            string? choice = Console.ReadLine();
            string[] validChoices = Constants.HitChoices.Concat(Constants.StandChoices).ToArray();

            while (choice == null || !validChoices.Contains(choice))
            {
                choice = Console.ReadLine();
            }

            return choice;
        }
    }
}
