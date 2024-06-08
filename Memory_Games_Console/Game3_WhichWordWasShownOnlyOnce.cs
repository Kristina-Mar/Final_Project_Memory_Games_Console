using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    internal class Game3_WhichWordWasShownOnlyOnce : BaseClassForAllGames
    {
        public override string[] ListOfWordsToBeShownToPlayer { get; set; } = new string[31];
        public override string[] GameSolution { get; set; } = new string[1];
        public override string[] PlayersAnswers { get; set; } = new string[1];
        public override double PlayerTime { get; set; } = 0;
        public override int PlayerScore { get; set; } = 0;

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
            PlayerTime = 0;
            PlayerScore = 0;
            int index;
            for (int i = 0; i < (ListOfWordsToBeShownToPlayer.Length - 1); i += 2)
            {
                index = GenerateNewIndex();
                while (ListOfWordsToBeShownToPlayer.Contains(allWords[index]))
                {
                    index = GenerateNewIndex();
                }
                ListOfWordsToBeShownToPlayer[i] = allWords[index];
                ListOfWordsToBeShownToPlayer[i+1] = allWords[index];
            }
            index = GenerateNewIndex();
            while (ListOfWordsToBeShownToPlayer.Contains(allWords[index]))
            {
                index = GenerateNewIndex();
            }
            GameSolution[0] = allWords[index];
            ListOfWordsToBeShownToPlayer[^1] = allWords[index];
            Random randomOrderGenerator = new Random();
            for (int j = ListOfWordsToBeShownToPlayer.Length - 1; j >= 0; j--)
            {
                string originalWord = ListOfWordsToBeShownToPlayer[j];
                int newIndex = randomOrderGenerator.Next(j + 1);
                ListOfWordsToBeShownToPlayer[j] = ListOfWordsToBeShownToPlayer[newIndex];
                ListOfWordsToBeShownToPlayer[newIndex] = originalWord;
            }
        }
        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 3: 31 words will be shown in the console one by one. One of them will only be shown once.");
            Console.WriteLine("Your task will be to correctly identify the one lone word.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            for (int i = 0; i < ListOfWordsToBeShownToPlayer.Count(); i++)
            {
                Console.Clear();
                Thread.Sleep(100);
                Console.Write($"{i + 1}: {ListOfWordsToBeShownToPlayer[i]}");
                Thread.Sleep(2000);
            }
            Console.Clear();
        }

        public override void LogPlayersAnswers()
        {
            Console.WriteLine("Which of the words appeared just once in the original list?");
            Console.WriteLine(string.Join(", ", ListOfWordsToBeShownToPlayer.Distinct().Order()));
            DateTime startTime = DateTime.Now;
            PlayersAnswers[0] = Console.ReadLine();
            PlayerTime = Math.Round((DateTime.Now - startTime).TotalSeconds, 2);
        }
        public override void CheckTheResults()
        {
            if (PlayersAnswers[0] == GameSolution[0])
            {
                PlayerScore = 1;
            }
        }
        public override void ShowTheResults()
        {
            if (PlayerScore == 1)
            {
                Console.WriteLine($"You're right, congratulations! Your time: {PlayerTime} s");
            }
            else
            {
                Console.WriteLine($"Incorrect, the right answer was {GameSolution[0]}.");
            }
            Console.ReadLine();
        }
    }
}
