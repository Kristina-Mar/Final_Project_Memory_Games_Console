using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public class PlayerScore
    {
        public int Score { get; private set; }
        public double Time { get; private set; }
        public string PlayerName { get; private set; }
        public static Dictionary<string , List<PlayerScore>> TopScoresOfAllGames {  get; private set; } = new Dictionary<string , List<PlayerScore>>();

        public PlayerScore(int score, double time)
        {
            Score = score;
            Time = time;
        }
        private static string GetPlayerName()
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
        private static void AddNewScoreToTopScores(string gameName, PlayerScore newScore)
        {
            newScore.PlayerName = GetPlayerName();
            TopScoresOfAllGames[gameName].Add(newScore);
        }

        public static void RemoveLowestScore(string gameName)
        {
            TopScoresOfAllGames[gameName] = TopScoresOfAllGames[gameName].OrderByDescending(p => p.Score).ThenBy(p => p.Time).ToList();
            TopScoresOfAllGames[gameName].RemoveAt(TopScoresOfAllGames[gameName].Count()-1);
        }

        public static void CheckTheScoreAgainstBestScores(string gameName, int playerScore, double playerTime)
        {
            PlayerScore newScore = new PlayerScore(playerScore, playerTime);
            if (!TopScoresOfAllGames.ContainsKey(gameName))
            {
                TopScoresOfAllGames.Add(gameName, new List<PlayerScore>());
                AddNewScoreToTopScores(gameName, newScore);
                return;
            }
            IEnumerable<PlayerScore> gameScores = TopScoresOfAllGames[gameName].OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            if (gameScores.Count() < 5)
            {
                AddNewScoreToTopScores(gameName, newScore);
            }
            else if (playerScore > gameScores.Last().Score)
            {
                AddNewScoreToTopScores(gameName, newScore);
                RemoveLowestScore(gameName);
            }
            else if (playerScore == gameScores.Last().Score && playerTime < gameScores.Last().Time)
            {
                AddNewScoreToTopScores(gameName, newScore);
                RemoveLowestScore(gameName);
            }
        }
        public static void ShowBestScoresForSpecificGame(string gameName)
        {
            if (!TopScoresOfAllGames.ContainsKey(gameName))
            {
                Console.WriteLine("There are no top scores for this game yet.");
                return;
            }
            var orderedScores = TopScoresOfAllGames[gameName].OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            Console.WriteLine($"Top scores for {gameName}:");
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).PlayerName}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
