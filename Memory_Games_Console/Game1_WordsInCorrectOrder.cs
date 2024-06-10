﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    public class Game1_WordsInCorrectOrder : BaseClassForAllGames
    {
        public override string GameName { get; set; } = "Game 1";
        public override string[] ListOfWordsToShowToPlayer { get; set; } = new string[10];
        public override string[] GameSolution { get; set; } = new string[10];
        public override string[] PlayerAnswers { get; set; } = new string[10];
        public override int PlayerScore { get; set; } = 0;
        public override double PlayerTime { get; set; } = 0;

        public override void PlayGame()
        {
            SetUpGame();
            DisplayGame();
            LogPlayerAnswers();
            CheckPlayerAnswers();
            ShowPlayerScore();
            Memory_Games_Console.PlayerScore.CheckTheScoreAgainstBestScores(GameName, PlayerScore, PlayerTime);
            Memory_Games_Console.PlayerScore.ShowBestScoresForSpecificGame(GameName);
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
        }
        
        public override void SetUpGame()
        {
            PlayerScore = 0;
            PlayerTime = 0;
            for (int i = 0; i < ListOfWordsToShowToPlayer.Length; i++)
            {
                string newWord = PickAWordFromListOfAllWords();
                while (ListOfWordsToShowToPlayer.Contains(newWord))
                {
                    newWord = PickAWordFromListOfAllWords();
                }
                ListOfWordsToShowToPlayer[i] = newWord;
            }
            GameSolution = ListOfWordsToShowToPlayer;
        }
        public override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Game 1: 10 words will flash in the console now. Your task is to remember their correct order.");
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            foreach (string word in ListOfWordsToShowToPlayer)
            {
                Console.Clear();
                Console.Write(word);
                Thread.Sleep(2000);
            }
            Console.Clear();
        }

        public override void LogPlayerAnswers()
        {
            Console.WriteLine("Write the words you have seen in the correct order:");
            Console.WriteLine(string.Join(", ", ListOfWordsToShowToPlayer.Order().ToArray()));
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < PlayerAnswers.Length; i++)
            {
                Console.WriteLine($"{i + 1}: ");
                PlayerAnswers[i] = Console.ReadLine();
            }
            PlayerTime = (DateTime.Now - startTime).TotalSeconds;
        }
        public override void CheckPlayerAnswers()
        {
            for (int i = 0; i < PlayerAnswers.Length; i++)
            {
                if (GameSolution[i] == PlayerAnswers[i])
                {
                    PlayerScore++;
                }
            }
        }
        public override void ShowPlayerScore()
        {
            Console.WriteLine($"Correct answers: {PlayerScore}, time: {(int)(PlayerTime / 60)} min {Math.Round(PlayerTime % 60, 2)} s");
        }
    }
}
