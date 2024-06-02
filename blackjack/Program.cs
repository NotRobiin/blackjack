namespace Blackjack;

class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

        Game game = new Game();
        bool keepPlaying = true;

        while (keepPlaying)
        {
            Console.Clear();
            game.Start();

            Console.WriteLine("Keep playing (Y) or exit (N)?:");

            string? choice = Console.ReadLine();

            if (!String.IsNullOrEmpty(choice) && choice.ToLower() == "y")
            {
                game.Restart();
                keepPlaying = true;
            }
            else
            {
                keepPlaying = false;
            }
        }
    }
}
