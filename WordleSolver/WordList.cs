using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordleSolver
{
    class WordList
    {
        public static string[] Answers = File.ReadAllLines("lists/answers.txt");
        public static string[] ValidGuesses = File.ReadAllLines("lists/validguesses.txt").Concat(Answers).ToArray();

        public static bool IsValidGuess(string word)
        {
            return ValidGuesses.Contains(word.ToLowerInvariant());
        }

        public static bool IsAnswer(string word)
        {
            return Answers.Contains(word.ToLowerInvariant());
        }

        public static string ProvideGuess()
        {
            List<string> perfectConditions = new List<string>();

            for (int i = 0; i < Answers.Length; i++)
            {
                string potentialWord = Answers[i];
                if (CheckWord(potentialWord))
                {
                    perfectConditions.Add(potentialWord);
                }
            }

            Console.WriteLine("ALL POTENTIAL ANSWERS");
            int currentHighestPriority = int.MaxValue;
            string highestCandidate = "";

            foreach (string s in perfectConditions)
            {
                Console.Write($"  {s.ToUpperInvariant()}");

                bool doubled = false;
                foreach (char c in s)
                {
                    if (s.Count(f => f == c) > 1)
                    {
                        Console.WriteLine(" (double)");
                        doubled = true;
                        break;
                    }
                }

                if (doubled)
                {
                    continue;
                }

                Console.WriteLine();

                int priority = 0;
                foreach (char character in s)
                {
                    priority += Array.IndexOf(Program.CommonLetters, character);
                }

                if (priority < currentHighestPriority)
                {
                    currentHighestPriority = priority;
                    highestCandidate = s;
                }
            }

            return highestCandidate;
        }

        private static bool CheckWord(string word)
        {
            for (int i = 0; i < Program.Alphabet.Length; i++)
            {
                if (Program.WordAccuracy[i] == Correctness.CorrectPosition)
                {
                    if (word[Program.Position[i]] != Program.Alphabet[i])
                    {
                        return false;
                    }
                }

                else if (Program.WordAccuracy[i] == Correctness.InWord)
                {
                    if (!word.Contains(Program.Alphabet[i]))
                    {
                        return false;
                    }

                    else if (Program.IncorrectPositions[i, word.IndexOf(Program.Alphabet[i])])
                    {
                        return false;
                    }
                }

                else if (Program.WordAccuracy[i] == Correctness.NotInWord)
                {
                    if (word.Contains(Program.Alphabet[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}