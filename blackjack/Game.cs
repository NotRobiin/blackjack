namespace Blackjack;

class Game
{
    private Deck _deck;
    private Player _player;
    private Player _dealer;
    private bool _isGameOver;
    private Constants.Turn _turn;
    private bool _isHoleRevealed;
    private bool _doubleDownAvailable;
    private Constants.WinResult _winResult;

    public Game()
    {
        _deck = new Deck(Constants.DecksAmount);
        _player = new Player();
        _dealer = new Player();
        _isGameOver = false;
        _turn = Constants.Turn.Player;
        _isHoleRevealed = false;
        _doubleDownAvailable = true;
    }

    public void Start()
    {
        _deck.Shuffle();
        DealInitialCards();
        _dealer.hand.GetCards().Last().Visible = false;
        CheckWin(Constants.WinScenario.Initial);

        while (!_isGameOver)
        {
            if (_turn == Constants.Turn.Dealer)
            {
                DealerTurn();
            }

            // Check if the game should end after double down.
            if (!_doubleDownAvailable && !_isGameOver)
            {
                CheckWin(Constants.WinScenario.DoubleDown);
            }

            if (_isGameOver)
            {
                break;
            }

            DisplayGameState();
            PlayerTurn();
            CheckWin(Constants.WinScenario.Regular);
        }

        GameOver();
    }

    public void Restart()
    {
        _deck = new Deck(Constants.DecksAmount);
        _player = new Player();
        _dealer = new Player();
        _isGameOver = false;
        _winResult = Constants.WinResult.None;
        _turn = Constants.Turn.Player;
        _isHoleRevealed = false;
        _doubleDownAvailable = true;

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
            HitUntilMinimumOrMoreThanPlayer(Constants.DealerMinimum);
            CheckWin(Constants.WinScenario.Reveal);
        }
        else
        {
            _dealer.hand.AddCard(_deck.DealCard());
            CheckWin(Constants.WinScenario.Regular);
        }
    }

    private void PlayerTurn()
    {
        string choice = _player.MakeChoice(_doubleDownAvailable);

        if (Constants.StandChoices.Contains(choice))
        {
            _turn = Constants.Turn.Dealer;
        }
        else if (Constants.HitChoices.Contains(choice))
        {
            _player.hand.AddCard(_deck.DealCard());
        }
        else if (Constants.DoubleDownChoices.Contains(choice))
        {
            // TODO: Double wager if any
            _player.hand.AddCard(_deck.DealCard());
            _doubleDownAvailable = false;
            _turn = Constants.Turn.Dealer;
        }
    }

    private void CheckWin(Constants.WinScenario scenario)
    {
        int playerScore = _player.hand.CalculateScore();
        int dealerScore = _dealer.hand.CalculateScore();

        Constants.WinResult result = scenario switch
        {
            Constants.WinScenario.Initial => Win.CheckInitial(playerScore, dealerScore),
            Constants.WinScenario.Regular => Win.CheckRegular(playerScore, dealerScore),
            Constants.WinScenario.DoubleDown => Win.CheckAfterDoubleDown(playerScore, dealerScore),
            Constants.WinScenario.Reveal => Win.CheckAfterReveal(playerScore, dealerScore),
            _ => throw new InvalidOperationException("Unexpected WinScenario")
        };

        if (result != Constants.WinResult.None)
        {
            _isGameOver = true;
            _winResult = result;
        }
    }

    private void RevealHoleCard()
    {
        _dealer.hand.GetLastCard().Visible = true;
        _isHoleRevealed = true;
    }

    private void HitUntilMinimumOrMoreThanPlayer(int minimum)
    {
        while (_dealer.hand.CalculateScore() < minimum || _dealer.hand.CalculateScore() < _player.hand.CalculateScore())
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

    private void DisplayGameState()
    {
        Console.Clear();

        _dealer.hand.Display();
        Console.WriteLine($"Dealer's hand: {_dealer.hand.FormatedScore(false)}");
        Console.WriteLine();

        if (_isGameOver)
        {
            Console.WriteLine("[GAME OVER]");
            Console.WriteLine(Constants.ResultMessages[_winResult]);
        }

        Console.WriteLine();
        Console.WriteLine($"Your hand: {_player.hand.FormatedScore(false)}");
        _player.hand.Display();
        Console.WriteLine();
    }
}
