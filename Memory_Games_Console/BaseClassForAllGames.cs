using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public abstract class BaseClassForAllGames
    {
        private readonly List<string> allWords = new List<string>() { "candle", "cat", "house", "home", "trolley", "mouse",
        "mousse", "field", "flood", "sun", "book", "chair", "music", "musical", "violet", "violin", "cello", "purple",
        "process", "pride", "private", "rain", "forest", "dive", "divide", "diamond", "whisper", "willow", "cloud",
        "wisp", "wasp", "waltz", "vote", "vole", "veto", "wind", "wire", "day", "dame", "deny", "close", "master", "meadow",
        "mirror", "custard", "click", "cumin", "horse"};

        public abstract string GameName { get; protected set; }
        public abstract string[] ListOfWordsToShowToPlayer { get; protected set; }
        public abstract string[] GameSolution { get; protected set; }
        public abstract string[] PlayerAnswers { get; protected set; }
        public abstract double PlayerTime { get; protected set; }
        public abstract int PlayerScore { get; protected set; }

        private Random _randomIndexGenerator = new Random();

        protected string PickAWordFromListOfAllWords()
        {
            int i = _randomIndexGenerator.Next(allWords.Count());
            return allWords[i];
        }

        public virtual void PlayGame()
        {
            SetUpGame();
            DisplayGame();
            LogPlayerAnswers();
            CheckPlayerAnswers();
            ShowPlayerScore();
            if (PlayerScore > 0)
            {
                Memory_Games_Console.PlayerScore.CheckTheScoreAgainstBestScores(GameName, PlayerScore, PlayerTime);
            }
            Memory_Games_Console.PlayerScore.ShowBestScoresForThisGame(GameName);
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
        }
        protected abstract void SetUpGame();
        protected abstract void DisplayGame();
        protected abstract void LogPlayerAnswers();
        protected abstract void CheckPlayerAnswers();
        protected abstract void ShowPlayerScore();
    }
}
