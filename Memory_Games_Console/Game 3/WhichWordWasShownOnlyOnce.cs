﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Games_Console
{
    internal class WhichWordWasShownOnlyOnce : BaseClassForAllGames
    {
        protected override string GameName { get; } = "Game 3";
        //public new const string GameName = "Game 3";
        public override string[] ListOfWordsToShowToPlayer { get; protected set; } = new string[31];
        public override string[] GameSolution { get; protected set; } = new string[1];
        public override string[] PlayerAnswers { get; protected set; } = new string[1];
        public override double PlayerTime { get; protected set; } = 0;
        public override int PlayerCorrectAnswers { get; protected set; } = 0;

        protected override void SetUpGame()
        {
            PlayerTime = 0;
            PlayerCorrectAnswers = 0;
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
            ListOfWordsToShowToPlayer[^1] = newWord;
            GameSolution[0] = newWord;

            Random randomOrderGenerator = new Random();
            for (int j = ListOfWordsToShowToPlayer.Length - 1; j >= 0; j--)
            {
                string originalWord = ListOfWordsToShowToPlayer[j];
                int newIndex = randomOrderGenerator.Next(j + 1);
                ListOfWordsToShowToPlayer[j] = ListOfWordsToShowToPlayer[newIndex];
                ListOfWordsToShowToPlayer[newIndex] = originalWord;
            }
        }
        protected override void DisplayGame()
        {
            Console.Clear();
            Console.WriteLine("Seeing double?");
            Console.WriteLine("31 words will be shown in the console one by one. One of them will only be shown once.");
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

        protected override void LogPlayerAnswers()
        {
            Console.WriteLine("Which of these words appeared just once in the original list?");
            Console.WriteLine(string.Join(", ", ListOfWordsToShowToPlayer.Distinct().Order()));
            DateTime startTime = DateTime.Now;
            PlayerAnswers[0] = Console.ReadLine();
            PlayerTime = (DateTime.Now - startTime).TotalSeconds;
        }

        protected override void CheckPlayerAnswers()
        {
            if (PlayerAnswers[0] == GameSolution[0])
            {
                PlayerCorrectAnswers = 1;
            }
        }

        protected override void ShowPlayerScore()
        {
            if (PlayerCorrectAnswers == 1)
            {
                Console.WriteLine($"You're right, congratulations! Your time: {TimeFormatting.FormatTime(PlayerTime)}");
            }
            else
            {
                Console.WriteLine($"Incorrect, the right answer was {GameSolution[0]}.");
            }
        }
    }
}
