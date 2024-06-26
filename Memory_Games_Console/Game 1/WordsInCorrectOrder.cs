﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    internal class WordsInCorrectOrder : BaseClassForAllGames
    {
        protected override string GameName { get; } = "Game 1";
        public override string[] ListOfWordsToShowToPlayer { get; protected set; } = new string[10];
        public override string[] GameSolution { get; protected set; } = new string[10];
        public override string[] PlayerAnswers { get; protected set; } = new string[10];
        public override int PlayerCorrectAnswers { get; protected set; } = 0;
        public override double PlayerTime { get; protected set; } = 0;
        
        protected override void SetUpGame()
        {
            PlayerCorrectAnswers = 0;
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

        protected override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Order, order!");
            Console.WriteLine("10 words will flash in the console now. Your task is to remember their correct order.");
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

        protected override void LogPlayerAnswers()
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

        protected override void CheckPlayerAnswers()
        {
            for (int i = 0; i < PlayerAnswers.Length; i++)
            {
                if (GameSolution[i] == PlayerAnswers[i])
                {
                    PlayerCorrectAnswers++;
                }
            }
        }

        protected override void ShowPlayerScore()
        {
            Console.WriteLine($"Correct answers: {PlayerCorrectAnswers}, time: {TimeFormatting.FormatTime(PlayerTime)}");
        }
    }
}
