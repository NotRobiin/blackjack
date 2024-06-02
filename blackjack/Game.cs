namespace Blackjack
{
    class Game
    {
        private Deck deck;
        private Player player;
        private Player dealer;
        private bool isGameOver;
        private bool playerWon;
        private bool isPush;
        private bool isPlayerTurn;
        private bool isHoleRevealed;

        public Game()
        {
            deck = new Deck(Constants.DecksAmount);
            player = new Player();
            dealer = new Player();
            isGameOver = false;
            playerWon = false;
            isPlayerTurn = true;
            isHoleRevealed = false;
        }

        public void Start()
        {
            deck.Shuffle();
            DealInitialCards();
            dealer.hand.GetCards().Last().Visible = false;
            CheckWin();

            while (!isGameOver)
            {
                if (!isPlayerTurn)
                {
                    DealerTurn();
                }

                CheckWin();

                if (isGameOver)
                {
                    break;
                }

                DisplayGameState();
                PlayerTurn();
            }

            GameOver();
        }

        public void Restart()
        {
            deck = new Deck(Constants.DecksAmount);
            player = new Player();
            dealer = new Player();
            isGameOver = false;
            playerWon = false;
            isPush = false;
            isPlayerTurn = true;
            isHoleRevealed = false;

            Console.Clear();
        }

        private void GameOver()
        {
            if (!isHoleRevealed)
            {
                RevealHoleCard();
            }

            DisplayGameState();
        }

        private void DealerTurn()
        {
            if (!isHoleRevealed)
            {
                RevealHoleCard();
                HitUntil17OrMoreThanPlayer();
                CheckAfterReveal();
            }
            else
            {
                dealer.hand.AddCard(deck.DealCard());
            }
        }

        private void PlayerTurn()
        {
            MakeChoice();
        }

        private void CheckWin()
        {
            int playerScore = player.hand.CalculateScore();

            if (playerScore == 21)
            {
                PlayerWin();
                return;
            }

            if (playerScore > 21)
            {
                DealerWin();
                return;
            }

            int dealerScore = dealer.hand.CalculateScore();

            if (dealerScore == 21)
            {
                DealerWin();
                return;
            }

            if (dealerScore > 21)
            {
                PlayerWin();
                return;
            }

            if (playerScore == dealerScore)
            {
                Push();
                return;
            }
        }

        private void CheckAfterReveal()
        {
            bool dealerBust = dealer.hand.IsBust();

            if (dealerBust)
            {
                PlayerWin();
                return;
            }

            int dealerScore = dealer.hand.CalculateScore();
            int playerScore = player.hand.CalculateScore();

            if (dealerScore > playerScore)
            {
                DealerWin();
                return;
            }
            else if (dealerScore < playerScore)
            {
                PlayerWin();
                return;
            }
            else if (playerScore == dealerScore)
            {
                Push();
                return;
            }
        }

        private void PlayerWin()
        {
            isGameOver = true;
            playerWon = true;
            isPush = false;
        }

        private void DealerWin()
        {
            isGameOver = true;
            playerWon = false;
            isPush = false;
        }

        private void Push()
        {
            isGameOver = true;
            playerWon = false;
            isPush = true;
        }


        private void RevealHoleCard()
        {
            if (isHoleRevealed)
            {
                return;
            }

            dealer.hand.GetLastCard().Visible = true;
            isHoleRevealed = true;
        }

        private void HitUntil17OrMoreThanPlayer()
        {
            while (dealer.hand.CalculateScore() < 17 || dealer.hand.CalculateScore() < player.hand.CalculateScore())
            {
                dealer.hand.AddCard(deck.DealCard());
            }
        }

        private void DealInitialCards()
        {
            player.hand.AddCard(deck.DealCard());
            dealer.hand.AddCard(deck.DealCard());
            player.hand.AddCard(deck.DealCard());
            dealer.hand.AddCard(deck.DealCard());
        }

        private void MakeChoice()
        {
            string choice = player.MakeChoice();

            if (Constants.StandChoices.Contains(choice))
            {
                isPlayerTurn = false;
            }
            else if (Constants.HitChoices.Contains(choice))
            {
                player.hand.AddCard(deck.DealCard());
            }
        }

        private void DisplayGameState()
        {
            Console.Clear();

            DisplayHand(dealer.hand);
            Console.WriteLine($"Dealer's hand: {dealer.hand.FormatedScore(false)}");
            Console.WriteLine();

            if (isGameOver)
            {
                Console.WriteLine("[GAME OVER]");

                if (isPush)
                {
                    Console.WriteLine("[PUSH]");
                }
                else
                {
                    Console.WriteLine("[{0} WINS]", playerWon ? "PLAYER" : "DEALER");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Your hand: {player.hand.FormatedScore(false)}");
            DisplayHand(player.hand);
            Console.WriteLine();
        }

        private void DisplayHand(Hand hand)
        {
            List<string[]> rows = [];

            foreach (Card card in hand.GetCards())
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
    }
}
