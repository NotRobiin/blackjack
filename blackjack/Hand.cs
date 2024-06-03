namespace Blackjack;

internal class Hand
{
    public Hand() => _hand = [];

    public void AddCard(Card card) => _hand.Add(card);

    public List<Card> GetCards() => _hand;

    public Card GetLastCard() => _hand[_hand.Count - 1];

    public bool IsWin() => CalculateScore() == Constants.BlackJack;

    public bool IsBust() => CalculateScore() > Constants.BlackJack;

    private List<Card> _hand;

    public int CalculateScore(bool includeInvisible = true)
    {
        int aceCount = includeInvisible
            ? _hand.Count(card => (card.Visible) && card.Value == "A")
            : _hand.Count(card => card.Value == "A");
        int score = _hand.Where(card => includeInvisible || card.Visible)
                        .Where(card => card.Value != "A")
                        .Sum(card => Constants.CardIntValues[card.Value]);

        // Add Aces
        for (int i = 0; i < aceCount; i++)
        {
            score += (score + Constants.AceHigh <= Constants.BlackJack ? Constants.AceHigh : Constants.AceLow);
        }

        return score;
    }

    public string FormatedScore(bool includeInvisible = true)
    {
        int score = CalculateScore(includeInvisible);

        if (score == Constants.BlackJack && _hand.Count == 2)
        {
            return "BLACKJACK";
        }
        else if (_hand.Count == 2 && HasInvisibleCard() && _hand.First().Value == "A")
        {
            return $"{Constants.AceLow}/{Constants.AceHigh}";
        }
        else if (GetVisibleAcesCount() > 0 && score + Constants.AceHigh - 1 <= Constants.BlackJack)
        {
            return $"{score}/{score + Constants.AceHigh - 1}";
        }
        else
        {
            return score.ToString();
        }
    }

    public void Display()
    {
        List<string[]> rows = [];

        foreach (Card card in GetCards())
        {
            string[] row = card.GetCardRepresentation().Split(Environment.NewLine);
            rows.Add(row);
        }

        for (int i = 0; i < Constants.CardHeight; i++)
        {
            foreach (var row in rows)
            {
                Console.Write(new string(' ', Constants.CardSpacingBetween));
                Console.Write(row[i]);
            }

            Console.WriteLine();
        }
    }

    private bool HasInvisibleCard() => _hand.Count(card => !card.Visible) > 0;

    private int GetVisibleAcesCount() => _hand.Count(card => card.Value == "A" && card.Visible);
}
