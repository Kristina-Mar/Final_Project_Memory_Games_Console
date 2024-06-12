using System.Security.Cryptography;

namespace Memory_Games_Console
{
    internal class Program
    {
        //public PlayerScores[] bestScores = new PlayerScores[15];
        private static void ShowGameSelection()
        {
            Console.Clear();
            Console.WriteLine("Which game would you like to play?");
            Console.WriteLine();
            Console.WriteLine("Game 1: Order, order!");
            Console.WriteLine("Remember 10 words I will show you and put them in the correct order.");
            Console.WriteLine();
            Console.WriteLine("Game 2: Find the imposters");
            Console.WriteLine("Take a look at a list of 30 words.");
            Console.WriteLine("I will then show you a selection of 10 words and you will have to correctly identify the ones that were not included in the original list.");
            Console.WriteLine();
            Console.WriteLine("Game 3: Seeing double?");
            Console.WriteLine("Find a lone word that only appeared once in the console.");
            Console.WriteLine();
            Console.WriteLine("Press 1 for Game 1, 2 for Game 2 or 3 for Game 3.");
            Console.WriteLine("Press Esc to close the program.");
        }
        static void Main(string[] args)
        {
            Game1_WordsInCorrectOrder game1 = new Game1_WordsInCorrectOrder();
            Game2_HaveYouSeenTheseWordsBefore game2 = new Game2_HaveYouSeenTheseWordsBefore();
            Game3_WhichWordWasShownOnlyOnce game3 = new Game3_WhichWordWasShownOnlyOnce();

            while (true)
            {
                ShowGameSelection();
                ConsoleKey gameChoice = Console.ReadKey().Key;
                switch (gameChoice)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        game1.PlayGame();
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        game2.PlayGame();
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        game3.PlayGame();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        ShowGameSelection();
                        break;
                }
            }
        }
    }
}
