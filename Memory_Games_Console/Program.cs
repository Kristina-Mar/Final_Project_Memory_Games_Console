namespace Memory_Games_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Game1_WordsInCorrectOrder game1setup = new Game1_WordsInCorrectOrder();
            //game1setup.PlayGame();
            Game2_HaveYouSeenTheseWordsBefore game2 = new Game2_HaveYouSeenTheseWordsBefore();
            game2.PlayGame();
            //Game3_WhichNumberWasShownOnlyOnce game3 = new Game3_WhichNumberWasShownOnlyOnce();
            //game3.PlayGame();
        }
    }
}
