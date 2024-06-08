using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    internal class Game2_HaveYouSeenTheseWordsBefore : BaseClassForAllGames
    {
        public override string[] ListOfWordsToBeShownToPlayer { get; set; } = new string[10];
        public override string[] GameSolution { get; set; } = new string[30];
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
            int index = GenerateNewIndex();
            for (int i = 0; i < GameSolution.Length; i++)
            {
                while (GameSolution.Contains(allWords[index]))
                {
                    index = GenerateNewIndex();
                }
                GameSolution[i] = allWords[index];
            }

            index = GenerateNewIndex();
            for (int i = 0; i < ListOfWordsToBeShownToPlayer.Length; i++)
            {
                while (ListOfWordsToBeShownToPlayer.Contains(allWords[index]))
                {
                    index = GenerateNewIndex();
                }
                ListOfWordsToBeShownToPlayer[i] = allWords[index];
            }
        }

        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 2: 30 words will be shown in the console. You have 30 seconds to remember them.");
            Console.WriteLine("Next, 10 words will be shown in the console and you will have to decide whether they were in the original list.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(string.Join(", ", GameSolution));
            Thread.Sleep(30000);
            Console.Clear();
        }

        public override void LogPlayersAnswers()
        {
            Console.WriteLine("Have you seen these words in the list? Type in Y or y for yes and N or n for no.");
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < ListOfWordsToBeShownToPlayer.Length; i++)
            {
                Console.WriteLine($"{ListOfWordsToBeShownToPlayer[i]}: ");
                string playersGuess = Console.ReadLine().ToUpper();
                while (playersGuess != "Y" && playersGuess != "N")
                {
                    Console.WriteLine("Try again, type in Y or y for yes and N or n for no.");
                    playersGuess = Console.ReadLine().ToUpper();
                }
                if ((GameSolution.Contains(ListOfWordsToBeShownToPlayer[i]) && playersGuess == "Y") || (!GameSolution.Contains(ListOfWordsToBeShownToPlayer[i]) && playersGuess == "N"))
                {
                    PlayersAnswers[i] = "correct";
                }
                else
                {
                    PlayersAnswers[i] = "incorrect";
                }
            }
            PlayerTime = (DateTime.Now - startTime).TotalSeconds;
        }
        public override void CheckTheResults()
        {
            PlayerScore = PlayersAnswers.Where(w => w.Equals("correct")).Count();
        }
        public override void ShowTheResults()
        {
            Console.WriteLine($"Correct answers: {PlayerScore}, time: {(int)(PlayerTime / 60)} min {Math.Round(PlayerTime % 60, 2)} s");
            Console.ReadLine();
        }
    }
}
