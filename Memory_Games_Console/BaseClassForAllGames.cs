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

        public abstract string GameName { get; set; }
        public abstract string[] ListOfWordsToShowToPlayer { get; set; }
        public abstract string[] GameSolution { get; set; }
        public abstract string[] PlayerAnswers { get; set; }
        public abstract double PlayerTime { get; set; }
        public abstract int PlayerScore { get; set; }

        private Random _randomIndexGenerator = new Random();

        public string PickAWordFromListOfAllWords()
        {
            int i = _randomIndexGenerator.Next(allWords.Count());
            return allWords[i];
        }

        public abstract void PlayGame();
        public abstract void SetUpGame();
        public abstract void DisplayGame();
        public abstract void LogPlayerAnswers();
        public abstract void CheckPlayerAnswers();
        public abstract void ShowPlayerScore();
    }
}
