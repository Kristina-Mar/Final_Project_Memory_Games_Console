using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    internal class HaveYouSeenTheseWordsBefore : BaseClassForAllGames
    {
        public new const string GameName = "Game 2";
        public override string[] ListOfWordsToShowToPlayer { get; protected set; } = new string[30];
        public override string[] GameSolution { get; protected set; } = new string[10];
        public override string[] PlayerAnswers { get; protected set; } = new string[10];
        public override int PlayerScore { get; protected set; } = 0;
        public override double PlayerTime { get; protected set; } = 0;

        protected override void SetUpGame()
        {
            PlayerScore = 0;
            PlayerTime = 0;
            string newWord = PickAWordFromListOfAllWords();
            for (int i = 0; i < ListOfWordsToShowToPlayer.Length; i++)
            {
                while (ListOfWordsToShowToPlayer.Contains(newWord))
                {
                    newWord = PickAWordFromListOfAllWords();
                }
                ListOfWordsToShowToPlayer[i] = newWord;
            }

            newWord = PickAWordFromListOfAllWords();
            for (int i = 0; i < GameSolution.Length; i++)
            {
                while (GameSolution.Contains(newWord))
                {
                    newWord = PickAWordFromListOfAllWords();
                }
                GameSolution[i] = newWord;
            }
        }

        protected override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 2: 30 words will be shown in the console. You have 30 seconds to remember them.");
            Console.WriteLine("After that, 10 words will be shown in the console and you will have to decide whether they were in the original list.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(string.Join(", ", ListOfWordsToShowToPlayer));
            Thread.Sleep(30000);
            Console.Clear();
        }

        protected override void LogPlayerAnswers()
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
                PlayerAnswers[i] = playersGuess.ToString();
                if (ListOfWordsToShowToPlayer.Contains(GameSolution[i]))
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
        protected override void CheckPlayerAnswers()
        {
            for (int i = 0; i < PlayerAnswers.Length; i++)
            {
                if (PlayerAnswers[i] == GameSolution[i])
                {
                    PlayerScore++;
                }
            }
        }
        protected override void ShowPlayerScore()
        {
            Console.WriteLine($"Correct answers: {PlayerScore}, time: {(int)(PlayerTime / 60)} min {Math.Round(PlayerTime % 60, 2)} s");
        }
    }
}
