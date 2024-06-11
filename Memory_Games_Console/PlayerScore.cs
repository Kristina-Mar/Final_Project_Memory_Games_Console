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
        public static Dictionary<string , PlayerScore[]> TopScoresOfAllGames {  get; private set; } = new Dictionary<string , PlayerScore[]>();

        public PlayerScore(string game, int score, double time)
        {
            Game = game;
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

        public static void CheckTheScoreAgainstBestScores(string gameName, int playerScore, double playerTime)
        {
            if (playerScore == 0)
            {
                return;
            }
            PlayerScore newScore = new PlayerScore(gameName, playerScore, playerTime);
            if (!TopScoresOfAllGames.ContainsKey(gameName))
            {
                TopScoresOfAllGames.Add(gameName, new PlayerScore[5]);
            }
            PlayerScore[] gameScores = TopScoresOfAllGames[gameName].OrderByDescending(p => p.Score).ThenBy(p => p.Time).ToArray();
            for (int i = 0; i < gameScores.Count(); i++)
            {
                if (gameScores[i] == null)
                {
                    newScore.Name = GetPlayerName();
                    gameScores[i] = newScore;
                    break;
                }
            }
            if (playerScore > gameScores.Last().Score)
            {
                newScore.Name = GetPlayerName();
                gameScores[^1] = newScore;
            }
            else if (playerScore == gameScores.Last().Score && playerTime < gameScores.Last().Time)
            {
                newScore.Name = GetPlayerName();
                gameScores[^1] = newScore;
            }
            TopScoresOfAllGames[gameName] = gameScores;
        }
        public static void ShowBestScoresForSpecificGame(string gameName)
        {
            if (!TopScoresOfAllGames.ContainsKey(gameName))
            {
                Console.WriteLine("There are no top scores for this game yet.");
                return;
            }
            var orderedScores = TopScoresOfAllGames[gameName].OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            Console.WriteLine($"Top 5 scores for {gameName}:");
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).Name}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
