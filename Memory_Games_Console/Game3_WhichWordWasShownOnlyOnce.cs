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
        public override string GameName { get; set; } = "Game 3";
        public override string[] ListOfWordsToShowToPlayer { get; set; } = new string[31];
        public override string[] GameSolution { get; set; } = new string[1];
        public override string[] PlayerAnswers { get; set; } = new string[1];
        public override double PlayerTime { get; set; } = 0;
        public override int PlayerScore { get; set; } = 0;

        public override void PlayGame()
        {
            SetUpGame();
            DisplayGame();
            LogPlayerAnswers();
            CheckPlayerAnswers();
            PlayerScores.CheckTheScoreAgainstBestScores(GameName, PlayerScore, PlayerTime);
            ShowPlayerScore();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
        }

        public override void SetUpGame()
        {
            PlayerTime = 0;
            PlayerScore = 0;
            string newWord;
            for (int i = 0; i < (ListOfWordsToShowToPlayer.Length - 1); i += 2)
            {
                newWord = PickAWordFromListOfAllWords();
                while (ListOfWordsToShowToPlayer.Contains(newWord))
                {
                    newWord = PickAWordFromListOfAllWords();
                }
                ListOfWordsToShowToPlayer[i] = newWord;
                ListOfWordsToShowToPlayer[i+1] = newWord;
            }
            newWord = PickAWordFromListOfAllWords();
            while (ListOfWordsToShowToPlayer.Contains(newWord))
            {
                newWord = PickAWordFromListOfAllWords();
            }
            GameSolution[0] = newWord;
            ListOfWordsToShowToPlayer[^1] = newWord;
            Random randomOrderGenerator = new Random();
            for (int j = ListOfWordsToShowToPlayer.Length - 1; j >= 0; j--)
            {
                string originalWord = ListOfWordsToShowToPlayer[j];
                int newIndex = randomOrderGenerator.Next(j + 1);
                ListOfWordsToShowToPlayer[j] = ListOfWordsToShowToPlayer[newIndex];
                ListOfWordsToShowToPlayer[newIndex] = originalWord;
            }
        }
        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 3: 31 words will be shown in the console one by one. One of them will only be shown once.");
            Console.WriteLine("Your task will be to correctly identify the one lone word.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            for (int i = 0; i < ListOfWordsToShowToPlayer.Count(); i++)
            {
                Console.Clear();
                Thread.Sleep(100);
                Console.Write($"{i + 1}: {ListOfWordsToShowToPlayer[i]}");
                Thread.Sleep(2000);
            }
            Console.Clear();
        }

        public override void LogPlayerAnswers()
        {
            Console.WriteLine("Which of the words appeared just once in the original list?");
            Console.WriteLine(string.Join(", ", ListOfWordsToShowToPlayer.Distinct().Order()));
            DateTime startTime = DateTime.Now;
            PlayerAnswers[0] = Console.ReadLine();
            PlayerTime = Math.Round((DateTime.Now - startTime).TotalSeconds, 2);
        }
        public override void CheckPlayerAnswers()
        {
            if (PlayerAnswers[0] == GameSolution[0])
            {
                PlayerScore = 1;
            }
        }
        public override void ShowPlayerScore()
        {
            if (PlayerScore == 1)
            {
                Console.WriteLine($"You're right, congratulations! Your time: {PlayerTime} s");
            }
            else
            {
                Console.WriteLine($"Incorrect, the right answer was {GameSolution[0]}.");
            }
            var orderedScores = PlayerScores.listOfAllBestScores.Where(p => p.Game == GameName)
            .OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).Name}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
