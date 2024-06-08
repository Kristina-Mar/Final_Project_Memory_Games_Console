using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public abstract class BaseClassForAllGames
    {
        public readonly List<string> allWords = new List<string>() { "candle", "cat", "house", "home", "trolley", "mouse",
        "mousse", "field", "flood", "sun", "book", "chair", "music", "musical", "violet", "violin", "cello", "purple",
        "process", "pride", "private", "rain", "forest", "dive", "divide", "diamond", "whisper", "willow", "cloud",
        "wisp", "wasp", "waltz", "vote", "vole", "veto", "wind", "wire", "day", "dame", "deny", "close", "master", "meadow",
        "mirror", "custard", "click", "cumin", "horse"};

        public abstract string[] ListOfWordsToBeShownToPlayer { get; set; }
        public abstract string[] GameSolution { get; set; }
        public abstract string[] PlayersAnswers { get; set; }
        public abstract double PlayerTime { get; set; }
        public abstract int PlayerScore { get; set; }

        public int GenerateNewIndex()
        {
            Random randomIndexGenerator = new Random();
            return randomIndexGenerator.Next(allWords.Count());
        }

        public abstract void PlayGame();
        public abstract void SetUpGame();
        public abstract void DisplayGame();
        public abstract void LogPlayersAnswers();
        public abstract void CheckTheResults();
        public abstract void ShowTheResults();
    }
}
