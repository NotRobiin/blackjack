namespace Blackjack;

internal class Win
{
    public static Constants.WinResult CheckWin(int playerScore, int dealerScore)
    {
        if (playerScore == Constants.BlackJack)
        {
            return Constants.WinResult.Player;
        }

        if (playerScore > Constants.BlackJack)
        {
            return Constants.WinResult.Dealer;
        }

        if (dealerScore == Constants.BlackJack)
        {
            return Constants.WinResult.Dealer;
        }

        if (dealerScore > Constants.BlackJack)
        {
            return Constants.WinResult.Player;
        }

        if (playerScore == dealerScore)
        {
            return Constants.WinResult.Push;
        }

        return Constants.WinResult.None;
    }
    
    public static Constants.WinResult CheckAfterReveal(int playerScore, int dealerScore)
    {
        if (dealerScore > Constants.BlackJack)
        {
            return Constants.WinResult.Player;
        }

        if (dealerScore > playerScore)
        {
            return Constants.WinResult.Dealer;
        }
        else if (dealerScore < playerScore)
        {
            return Constants.WinResult.Player;
        }
        else if (playerScore == dealerScore)
        {
            return Constants.WinResult.Push;
        }

        return Constants.WinResult.None;
    }

    public static Constants.WinResult CheckAfterDoubleDown(int playerScore, int dealerScore)
    {
        if (dealerScore == Constants.BlackJack)
        {
            return Constants.WinResult.Dealer;
        }
        else if (playerScore == Constants.BlackJack)
        {
            return Constants.WinResult.Player;
        }

        if (dealerScore > playerScore)
        {
            return Constants.WinResult.Dealer;
        }
        else if (dealerScore < playerScore)
        {
            return Constants.WinResult.Player;
        }
        else if (dealerScore == playerScore)
        {
            return Constants.WinResult.Push;
        }

        return Constants.WinResult.None;
    }
}
