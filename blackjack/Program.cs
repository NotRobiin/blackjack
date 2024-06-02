using System.Runtime.InteropServices;

namespace Blackjack;

class Program
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    static void Main(string[] args)
    {
        IntPtr handle = GetConsoleWindow();

        ShowWindow(handle, 3);

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
