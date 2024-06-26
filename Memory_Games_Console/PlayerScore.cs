﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Memory_Games_Console
{
    [DataContract]
    public class PlayerScore
    {
        [DataMember]
        public string GameName { get; private set; }
        [DataMember]
        public int CorrectAnswers { get; private set; }
        [DataMember]
        public double Time { get; private set; }
        [DataMember]
        public string PlayerName { get; private set; }
        [DataMember]
        private static List<PlayerScore> _topScores {  get; set; } = new List<PlayerScore>();
        private static readonly string _scoresFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Memory_Games_Console_data", "TopScores");
        private static string _topScoresFilePath = string.Empty;
        private static readonly DataContractSerializer serializer = new DataContractSerializer(typeof(List<PlayerScore>));
        public PlayerScore()
        {
            
        }

        public PlayerScore(string gameName, int correctAnswers, double time)
        {
            GameName = gameName;
            CorrectAnswers = correctAnswers;
            Time = time;
        }

        private string GetPlayerName()
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

        private void AddNewScoreToTopScores()
        {
            PlayerName = GetPlayerName();
            _topScores.Add(this);
        }

        private static void RemoveLowestScore(string gameName)
            // Only top 5 scores are saved.
        {
            _topScores = _topScores.OrderByDescending(p => p.CorrectAnswers).ThenBy(p => p.Time).ToList();
            _topScores.RemoveAt(_topScores.Count() - 1);
        }

        private static void SetValidTopScoreFilePath(string gameName)
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
        }

        private static List<PlayerScore> LoadBestScoresFromFile(string gameName)
        { 
            SetValidTopScoreFilePath(gameName);
            if (File.ReadAllText(_topScoresFilePath).Length != 0)
            {
                using (FileStream fileStream = new FileStream(_topScoresFilePath, FileMode.Open))
                {
                    using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                    {
                        _topScores = (List<PlayerScore>)serializer.ReadObject(reader, true);
                    }
                }
            }
            else
            {
                _topScores.Clear(); // So that the program doesn't use top scores from another game.
            }
            return _topScores;
        }

        private static void SaveBestScoresToFile(string gameName)
        {
            SetValidTopScoreFilePath(gameName);
            if (!File.Exists(_topScoresFilePath))
            {
                File.Create(_topScoresFilePath).Close();
            }
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            };
            using (XmlWriter writer = XmlWriter.Create(_topScoresFilePath, settings))
            {
                serializer.WriteObject(writer, _topScores);
            }
        }

        public void CheckTheScoreAgainstBestScores()
        {
            _topScores = LoadBestScoresFromFile(GameName).OrderByDescending(p => p.CorrectAnswers).ThenBy(p => p.Time).ToList();
            IEnumerable<PlayerScore> gameScores = _topScores.OrderByDescending(p => p.CorrectAnswers).ThenBy(p => p.Time);
            if (gameScores.Count() < 5)
            {
            }
            else if (CorrectAnswers > gameScores.Last().CorrectAnswers)
            {
                RemoveLowestScore(GameName);
            }
            else if (CorrectAnswers == gameScores.Last().CorrectAnswers && Time < gameScores.Last().Time)
            {
                RemoveLowestScore(GameName);
            }
            else
            {
                return;
            }
            AddNewScoreToTopScores();
            SaveBestScoresToFile(GameName);
        }

        public static void ShowBestScoresForThisGame(string gameName)
        {
            _topScores = LoadBestScoresFromFile(gameName);
            if (_topScores.Count() == 0)
            {
                Console.WriteLine("There are no top scores for this game yet.");
                return;
            }
            var orderedScores = _topScores.OrderByDescending(p => p.CorrectAnswers).ThenBy(p => p.Time);
            Console.WriteLine($"Top scores for {gameName}:");
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).PlayerName}, score: {orderedScores.ElementAt(i).CorrectAnswers}, time: {TimeFormatting.FormatTime(orderedScores.ElementAt(i).Time)}");
            }
        }
    }
}
