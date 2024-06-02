namespace Blackjack
{
    internal class Hand
    {
        private List<Card> hand { get; set; }

        public Hand() => hand = [];

        public void AddCard(Card card) => hand.Add(card);

        public List<Card> GetCards() => hand;

        public Card GetLastCard() => hand[hand.Count - 1];

        public bool IsWin() => CalculateScore() == 21;

        public bool IsBust() => CalculateScore() > 21;

        private bool HasInvisibleCard() => hand.Count(card => !card.Visible) > 0;

        private int GetVisibleAcesCount() => hand.Count(card => card.Value == "A" && card.Visible);

        public int CalculateScore(bool includeInvisible = true)
        {
            int score = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                if (!includeInvisible && !card.Visible)
                {
                    continue;
                }

                if (card.Value == "A")
                {
                    aceCount++;

                    continue;
                }

                score += Constants.CardIntValues[card.Value];
            }

            // Add Aces
            for (int i = 0; i < aceCount; i++)
            {
                score += (score + 11 <= 21 ? 11 : 1);
            }

            return score;
        }

        public string FormatedScore(bool includeInvisible = true)
        {
            int score = CalculateScore(includeInvisible);

            if (score == 21 && hand.Count == 2)
            {
                return "BLACKJACK";
            }
            else if (hand.Count == 2 && HasInvisibleCard() && hand.First().Value == "A")
            {
                return "1/11";
            }
            else if (GetVisibleAcesCount() > 0 && score + 10 <= 21)
            {
                return $"{score}/{score + 10}";
            }
            else
            {
                return score.ToString();
            }
        }
    }
}
