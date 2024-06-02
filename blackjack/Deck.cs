namespace Blackjack;

class Deck
{
    private List<Card> _cards;

    public Deck(int numDecks)
    {
        _cards = [];

        for (int i = 0; i < numDecks; i++)
        {
            InitializeDeck();
        }
    }

    private void InitializeDeck()
    {
        foreach (string suit in Constants.CardSuits)
        {
            foreach (string value in Constants.CardValues)
            {
                _cards.Add(new Card(value, suit));
            }
        }
    }

    public void Shuffle()
    {
        _cards = _cards.OrderBy(x => Random.Shared.Next()).ToList();
    }

    public Card DealCard()
    {
        Card cardToDeal = _cards.First();
        _cards.Remove(cardToDeal);

        return cardToDeal;
    }
}
