using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public class PlayerScore
    {
        public string Game { get; private set; }
        public int Score { get; private set; }
        public double Time { get; private set; }
        public string Name { get; private set; }

        public static List<PlayerScore> listOfAllBestScores = new List<PlayerScore>();

        public PlayerScore(string game, int score, double time)
        {
            Game = game;
            Score = score;
            Time = time;
        }
        public static string GetPlayerName()
        {
            Console.WriteLine("Congratulations! You've made it onto the list of best players! Please enter your name.");
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty, please enter your name.");
                name = Console.ReadLine();
            }
            return name;
        }

        public static void CheckTheScoreAgainstBestScores(string gameName, int playerScore, double playerTime)
        {
            /*if (playerScore == 0)
            {
                return;
            }
            IEnumerable<PlayerScores> gameScores = listOfAllBestScores.Where(p => p.Game == gameName)
            .OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            PlayerScores newScore = new PlayerScores(gameName, playerScore, playerTime);
            if (gameScores.Count() < 5)
            {
                newScore.Name = GetPlayersName();
                listOfAllBestScores.Add(newScore);
                return;
            }
            if (playerScore > gameScores.Last().Score)
            {
                newScore.Name = GetPlayersName();
                listOfAllBestScores.RemoveAt(-1);
                listOfAllBestScores.Add(newScore);
            }
            else if (playerScore == gameScores.Last().Score && playerTime < gameScores.Last().Time)
            {
                newScore.Name = GetPlayersName();
                listOfAllBestScores.Add(newScore);
            }*/
        }
        public static void ShowBestScoresForSpecificGame(string gameName)
        {
            var orderedScores = PlayerScore.listOfAllBestScores.Where(p => p.Game == gameName)
                .OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            Console.WriteLine($"Top 5 scores for {gameName}:");
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).Name}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
