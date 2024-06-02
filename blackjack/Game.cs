namespace Blackjack;

class Game
{
    private Deck _deck;
    private Player _player;
    private Player _dealer;
    private bool _isGameOver;
    private bool _playerWon;
    private bool _isPush;
    private bool _isPlayerTurn;
    private bool _isHoleRevealed;

    public Game()
    {
        _deck = new Deck(Constants.DecksAmount);
        _player = new Player();
        _dealer = new Player();
        _isGameOver = false;
        _playerWon = false;
        _isPlayerTurn = true;
        _isHoleRevealed = false;
    }

    public void Start()
    {
        _deck.Shuffle();
        DealInitialCards();
        _dealer.hand.GetCards().Last().Visible = false;
        CheckWin();

        while (!_isGameOver)
        {
            if (!_isPlayerTurn)
            {
                DealerTurn();
            }

            CheckWin();

            if (_isGameOver)
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
        _deck = new Deck(Constants.DecksAmount);
        _player = new Player();
        _dealer = new Player();
        _isGameOver = false;
        _playerWon = false;
        _isPush = false;
        _isPlayerTurn = true;
        _isHoleRevealed = false;

        Console.Clear();
    }

    private void GameOver()
    {
        if (!_isHoleRevealed)
        {
            RevealHoleCard();
        }

        DisplayGameState();
    }

    private void DealerTurn()
    {
        if (!_isHoleRevealed)
        {
            RevealHoleCard();
            HitUntilMinimumOrMoreThanPlayer();
            CheckAfterReveal();
        }
        else
        {
            _dealer.hand.AddCard(_deck.DealCard());
        }
    }

    private void PlayerTurn()
    {
        MakeChoice();
    }

    private void CheckWin()
    {
        int playerScore = _player.hand.CalculateScore();

        if (playerScore == Constants.BlackJack)
        {
            PlayerWin();
            return;
        }

        if (playerScore > Constants.BlackJack)
        {
            DealerWin();
            return;
        }

        int dealerScore = _dealer.hand.CalculateScore();

        if (dealerScore == Constants.BlackJack)
        {
            DealerWin();
            return;
        }

        if (dealerScore > Constants.BlackJack)
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
        bool dealerBust = _dealer.hand.IsBust();

        if (dealerBust)
        {
            PlayerWin();
            return;
        }

        int dealerScore = _dealer.hand.CalculateScore();
        int playerScore = _player.hand.CalculateScore();

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
        _isGameOver = true;
        _playerWon = true;
        _isPush = false;
    }

    private void DealerWin()
    {
        _isGameOver = true;
        _playerWon = false;
        _isPush = false;
    }

    private void Push()
    {
        _isGameOver = true;
        _playerWon = false;
        _isPush = true;
    }


    private void RevealHoleCard()
    {
        if (_isHoleRevealed)
        {
            return;
        }

        _dealer.hand.GetLastCard().Visible = true;
        _isHoleRevealed = true;
    }

    private void HitUntilMinimumOrMoreThanPlayer()
    {
        while (_dealer.hand.CalculateScore() < Constants.DealerMinimum || _dealer.hand.CalculateScore() < _player.hand.CalculateScore())
        {
            _dealer.hand.AddCard(_deck.DealCard());
        }
    }

    private void DealInitialCards()
    {
        _player.hand.AddCard(_deck.DealCard());
        _dealer.hand.AddCard(_deck.DealCard());
        _player.hand.AddCard(_deck.DealCard());
        _dealer.hand.AddCard(_deck.DealCard());
    }

    private void MakeChoice()
    {
        string choice = _player.MakeChoice();

        if (Constants.StandChoices.Contains(choice))
        {
            _isPlayerTurn = false;
        }
        else if (Constants.HitChoices.Contains(choice))
        {
            _player.hand.AddCard(_deck.DealCard());
        }
    }

    private void DisplayGameState()
    {
        Console.Clear();

        DisplayHand(_dealer.hand);
        Console.WriteLine($"Dealer's hand: {_dealer.hand.FormatedScore(false)}");
        Console.WriteLine();

        if (_isGameOver)
        {
            Console.WriteLine("[GAME OVER]");

            if (_isPush)
            {
                Console.WriteLine("[PUSH]");
            }
            else
            {
                Console.WriteLine("[{0} WINS]", _playerWon ? "PLAYER" : "DEALER");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Your hand: {_player.hand.FormatedScore(false)}");
        DisplayHand(_player.hand);
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
