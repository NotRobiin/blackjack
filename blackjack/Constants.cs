namespace Blackjack;

class Constants
{
    public enum WinScenario
    {
        Initial,
        Regular,
        DoubleDown,
        Reveal
    };

    public enum WinResult
    {
        None,
        Player,
        Dealer,
        Push
    };

    public enum Turn
    {
        Player,
        Dealer
    };

    public static readonly string[] CardSuits = { "Hearts", "Diamonds", "Clubs", "Spades" };
    public static readonly string[] CardValues = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    public static readonly Dictionary<string, int> CardValueToScore = new Dictionary<string, int>
    {
        { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 }, { "7", 7 },
        { "8", 8 }, { "9", 9 }, { "10", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 11 }
    };
    public const int CardWidth = 16;
    public const int CardHeight = 11;
    public const int CardSpacingBetween = 5;
    public static readonly Dictionary<string, string> SuitSymbols = new Dictionary<string, string>
    {
        { "Hearts", "♥" },
        { "Diamonds", "♦" },
        { "Clubs", "♣" },
        { "Spades", "♠" }
    };
    public static readonly string[] HitChoices = ["h", "hit"];
    public static readonly string[] StandChoices = ["s", "stand"];
    public static readonly string[] DoubleDownChoices = ["d", "double", "dd"];

    public static readonly Dictionary<WinResult, string> ResultMessages = new Dictionary<WinResult, string>
    {
        { WinResult.Player, "[PLAYER WINS]" },
        { WinResult.Dealer, "[DEALER WINS]" },
        { WinResult.Push, "[PUSH]" }
    };

    public const int DecksAmount = 1;

    public const int AceHigh = 11;
    public const int AceLow = 1;
    public const int BlackJack = 21;
    public const int DealerMinimum = 17;
}
