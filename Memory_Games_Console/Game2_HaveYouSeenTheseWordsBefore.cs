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
        public override string GameName { get; set; } = "Game 2";
        public override string[] ListOfWordsToBeShownToPlayer { get; set; } = new string[30];
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
            PlayerScores.CheckTheScoreAgainstBestScores(GameName, PlayerScore, PlayerTime);
            ShowTheResults();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
        }

        public override void SetUpGame()
        {
            PlayerScore = 0;
            PlayerTime = 0;
            string newWord = GenerateNewWord();
            for (int i = 0; i < ListOfWordsToBeShownToPlayer.Length; i++)
            {
                while (ListOfWordsToBeShownToPlayer.Contains(newWord))
                {
                    newWord = GenerateNewWord();
                }
                ListOfWordsToBeShownToPlayer[i] = newWord;
            }

            newWord = GenerateNewWord();
            for (int i = 0; i < GameSolution.Length; i++)
            {
                while (GameSolution.Contains(newWord))
                {
                    newWord = GenerateNewWord();
                }
                GameSolution[i] = newWord;
            }
        }

        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 2: 30 words will be shown in the console. You have 30 seconds to remember them.");
            Console.WriteLine("After that, 10 words will be shown in the console and you will have to decide whether they were in the original list.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(string.Join(", ", ListOfWordsToBeShownToPlayer));
            Thread.Sleep(30000);
            Console.Clear();
        }

        public override void LogPlayersAnswers()
        {
            Console.WriteLine("Have you seen these words in the list? Type in Y or y for yes and N or n for no.");
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < GameSolution.Length; i++)
            {
                Console.WriteLine($"{GameSolution[i]}: ");
                char playersGuess = char.ToUpper(Console.ReadKey().KeyChar);
                while (playersGuess != 'Y' && playersGuess != 'N')
                {
                    Console.WriteLine();
                    Console.WriteLine("Try again, type in Y or y for yes and N or n for no.");
                    playersGuess = char.ToUpper(Console.ReadKey().KeyChar);
                }
                PlayersAnswers[i] = playersGuess.ToString();
                if (ListOfWordsToBeShownToPlayer.Contains(GameSolution[i]))
                {
                    GameSolution[i] = "Y";
                }
                else
                {
                    GameSolution[i] = "N";
                }
                Console.WriteLine();
            }
            PlayerTime = (DateTime.Now - startTime).TotalSeconds;
        }
        public override void CheckTheResults()
        {
            for (int i = 0; i < PlayersAnswers.Length; i++)
            {
                if (PlayersAnswers[i] == GameSolution[i])
                {
                    PlayerScore++;
                }
            }
        }
        public override void ShowTheResults()
        {
            Console.WriteLine($"Correct answers: {PlayerScore}, time: {(int)(PlayerTime / 60)} min {Math.Round(PlayerTime % 60, 2)} s");
            var orderedScores = PlayerScores.listOfAllBestScores.Where(p => p.Game == GameName)
            .OrderByDescending(p => p.Score).ThenBy(p => p.Time);
            for (int i = 0; i < orderedScores.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Name: {orderedScores.ElementAt(i).Name}, score: {orderedScores.ElementAt(i).Score}, time: {orderedScores.ElementAt(i).Time}");
            }
        }
    }
}
