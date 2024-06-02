using System.Text;

namespace Blackjack
{
    public class Card
    {
        public string Value { get; set; }
        public string Suit { get; set; }
        public bool Visible { get; set; }

        public Card(string value, string suit)
        {
            Value = value;
            Suit = suit;
            Visible = true;
        }

        public string GetCardRepresentation()
        {
            string value = Visible ? Value : "?";
            string suit = Visible ? Suit : "?";

            int spacingSide = 2;
            int topPadding = (Constants.CardHeight - 4) / 2;
            int bottomPadding = topPadding;
            string suitSymbol = suit == "?" ? "?" : Constants.SuitSymbols[suit];
            int valuePaddingRight = Constants.CardWidth - spacingSide - value.Length - 1;
            int suitPaddingLeft = (Constants.CardWidth - suitSymbol.Length - spacingSide) / 2;
            int suitPaddingRight = suitPaddingLeft + (Constants.CardWidth - suitSymbol.Length - 2) % 2;

            StringBuilder cardBuilder = new StringBuilder();

            // Top left value
            cardBuilder.AppendLine($"┌{FillWith('─', Constants.CardWidth - 2)}┐");
            cardBuilder.AppendLine($"│ {value}{FillWith(' ', valuePaddingRight)}│");

            // Top padding
            for (int i = 0; i < topPadding; i++)
            {
                cardBuilder.AppendLine($"│{FillWith(' ', Constants.CardWidth - 2)}│");
            }

            // Middle suit
            cardBuilder.AppendLine($"│{FillWith(' ', suitPaddingLeft)}{suitSymbol}{FillWith(' ', suitPaddingRight)}│");

            // Bottom padding
            for (int i = 0; i < bottomPadding; i++)
            {
                cardBuilder.AppendLine($"│{FillWith(' ', Constants.CardWidth - 2)}│");
            }

            // Bottom right value
            cardBuilder.AppendLine($"│{FillWith(' ', Constants.CardWidth - 3 - value.Length)}{value} │");
            cardBuilder.AppendLine($"└{FillWith('─', Constants.CardWidth - 2)}┘");

            return cardBuilder.ToString();
        }

        private string FillWith(char what, int amount)
        {
            return new string(what, amount);
        }
    }
}