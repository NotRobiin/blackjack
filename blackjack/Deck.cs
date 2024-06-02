namespace Blackjack
{
    class Deck
    {
        private List<Card> cards;

        public Deck(int numDecks)
        {
            cards = [];

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
                    cards.Add(new Card(value, suit));
                }
            }
        }

        public void Shuffle()
        {
            cards = cards.OrderBy(x => Random.Shared.Next()).ToList();
        }

        public Card DealCard()
        {
            Card cardToDeal = cards[0];
            cards.RemoveAt(0);

            return cardToDeal;
        }
    }
}
