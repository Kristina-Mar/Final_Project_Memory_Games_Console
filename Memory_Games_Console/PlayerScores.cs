using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public class PlayerScores
    {
        public string Game { get; private set; }
        public int Score { get; private set; }
        public double Time { get; private set; }
        public string Name { get; private set; }

        public static List<PlayerScores> listOfAllBestScores = new List<PlayerScores>();

        public PlayerScores(string game, int score, double time)
        {
            Game = game;
            Score = score;
            Time = time;
        }
        public static string GetPlayersName()
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
                listOfAllBestScores.Add(newScore);
            }
            else if (playerScore == gameScores.Last().Score && playerTime < gameScores.Last().Time)
            {
                newScore.Name = GetPlayersName();
                listOfAllBestScores.Add(newScore);
            }
        }
    }
}
