using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public class Game1_WordsInCorrectOrder : BaseClassForAllGames
    {
        public override string[] ListOfWordsToBeShownToPlayer { get; set; } = new string[10];
        public override string[] GameSolution { get; set; } = new string[10];
        public override string[] PlayersAnswers { get; set; } = new string[10];
        public override int PlayerScore { get; set; } = 0;
        public override double PlayerTime { get; set; } = 0;

        public override void PlayGame()
        {
            SetUpGame();
            DisplayGame();
            LogPlayersAnswers();
            CheckTheResults();
            ShowTheResults();
        }
        
        public override void SetUpGame()
        {
            PlayerScore = 0;
            PlayerTime = 0;
            for (int i = 0; i < GameSolution.Length; i++)
            {
                int index = GenerateNewIndex();
                while (GameSolution.Contains(allWords[index]))
                {
                    index = GenerateNewIndex();
                }
                GameSolution[i] = allWords[index];
            }
            ListOfWordsToBeShownToPlayer = GameSolution;
        }
        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 1: 10 words will flash in the console now. Your task is to remember their correct order.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            foreach (string word in ListOfWordsToBeShownToPlayer)
            {
                Console.Clear();
                Console.Write(word);
                Thread.Sleep(2000);
            }
            Console.Clear();
        }

        public override void LogPlayersAnswers()
        {
            Console.WriteLine("Write the words you have seen in the correct order:");
            Console.WriteLine(string.Join(", ", ListOfWordsToBeShownToPlayer.Order().ToArray()));
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < PlayersAnswers.Length; i++)
            {
                Console.WriteLine($"{i + 1}: ");
                PlayersAnswers[i] = Console.ReadLine();
            }
            PlayerTime = (DateTime.Now - startTime).TotalSeconds;
        }
        public override void CheckTheResults()
        {
            for (int i = 0; i < PlayersAnswers.Length; i++)
            {
                if (GameSolution[i] == PlayersAnswers[i])
                {
                    PlayerScore++;
                }
            }
        }
        public override void ShowTheResults()
        {
            Console.WriteLine($"Correct answers: {PlayerScore}, time: {(int)(PlayerTime / 60)} min {Math.Round(PlayerTime % 60, 2)} s");
            Console.ReadLine();
        }
    }
}
