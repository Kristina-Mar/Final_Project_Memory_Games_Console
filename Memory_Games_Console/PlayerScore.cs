using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Memory_Games_Console
{
    public class PlayerScore
    {
        public string GameName { get; set; }
        public int Score { get; set; }
        public double Time { get; set; }
        public string PlayerName { get; set; }
        private static List<PlayerScore> _topScores {  get; set; } = new List<PlayerScore>();
        private static readonly string _scoresFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Memory_Games_Console_data", "TopScores");
        private static string _topScoresFilePath = string.Empty;
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerScore>));
        public PlayerScore()
        {
            
        }

        public PlayerScore(string gameName, int score, double time)
        {
            GameName = gameName;
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
        private static void AddNewScoreToTopScores(PlayerScore newScore)
        {
            newScore.PlayerName = GetPlayerName();
            _topScores.Add(newScore);
        }

        public static void RemoveLowestScore(string gameName)
        {
            _topScores = _topScores.OrderByDescending(p => p.Score).ThenBy(p => p.Time).ToList();
            _topScores.RemoveAt(_topScores.Count() - 1);
        }

        public static List<PlayerScore> LoadBestScoresFromFile(string gameName)
        { 
            if (!Directory.Exists(_scoresFolderPath))
            {
                Directory.CreateDirectory(_scoresFolderPath);
            }
            _topScoresFilePath = Path.Combine(_scoresFolderPath, $"{gameName}_TopScores.txt");
            if (!File.Exists(_topScoresFilePath))
            {
                File.Create(_topScoresFilePath).Close();
            }
            if (File.ReadAllText(_topScoresFilePath).Length != 0)
            {
                using (StreamReader readerXML = new StreamReader(_topScoresFilePath))
                {
                    _topScores = serializer.Deserialize(readerXML) as List<PlayerScore>;
                }
            }
            else
            {
                _topScores.Clear();
            }
            return _topScores;
        }
        public static void SaveBestScoresFromFile(string gameName)
        {
            if (!Directory.Exists(_scoresFolderPath))
            {
                Directory.CreateDirectory(_scoresFolderPath);
            }
            _topScoresFilePath = Path.Combine(_scoresFolderPath, $"{gameName}_TopScores.txt");
            if (!File.Exists(_topScoresFilePath))
            {
                File.Create(_topScoresFilePath).Close();
            }
            using (StreamWriter writerXML = new StreamWriter(_topScoresFilePath))
            {
                serializer.Serialize(writerXML, _topScores);
            }
        }

        public static void CheckTheScoreAgainstBestScores(string gameName, int playerScore, double playerTime)
        {
            PlayerScore newScore = new PlayerScore(gameName, playerScore, playerTime);
            _topScores = LoadBestScoresFromFile(gameName).OrderByDescending(p => p.Score).ThenBy(p => p.Time).ToList();
            IEnumerable<PlayerScore> gameScores = _topScores.OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            if (gameScores.Count() < 5)
            {
            }
            else if (playerScore > gameScores.Last().Score)
            {
                RemoveLowestScore(gameName);
            }
            else if (playerScore == gameScores.Last().Score && playerTime < gameScores.Last().Time)
            {
                RemoveLowestScore(gameName);
            }
            else
            {
                return;
            }
            AddNewScoreToTopScores(newScore);
            SaveBestScoresFromFile(gameName);
        }
        public static void ShowBestScoresForSpecificGame(string gameName)
        {
            _topScores = LoadBestScoresFromFile(gameName);
            if (_topScores.Count() == 0)
            {
                Console.WriteLine("There are no top scores for this game yet.");
                return;
            }
            var orderedScores = _topScores.OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            Console.WriteLine($"Top scores for {gameName}:");
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).PlayerName}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
