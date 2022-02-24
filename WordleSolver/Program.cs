using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleSolver
{
    class Program
    {
        public static int Guess = 0;
        public static char[] Alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i).ToArray();
        public static char[] CommonLetters = "eariotnslcupdmhgbfywkvxzjq".ToCharArray();

        public static Correctness[] WordAccuracy = new Correctness[26];
        public static int[] Position = new int[26];
        public static bool[,] IncorrectPositions = new bool[26, 5];

        static void Main(string[] args)
        {
            Console.WriteLine("Wordle Solver");

            for (int i = 0; i < 6; i++)
            {
                Guess++;
                string word = GuessWord();
                Console.WriteLine();
                Console.WriteLine($"Guess: {word.ToUpperInvariant()}");
                
                Console.Write("Input results of guess in gyn: ");
                string gyn = Console.ReadLine();
                for (int j = 0; j < 5; j++)
                {
                    int letterindex = Array.IndexOf(Alphabet, word[j].ToString().ToLowerInvariant()[0]);
                    if (gyn[j] == 'g')
                    {
                        WordAccuracy[letterindex] = Correctness.CorrectPosition;
                        Position[letterindex] = j;
                    }

                    else if (gyn[j] == 'y')
                    {
                        if (WordAccuracy[letterindex] != Correctness.CorrectPosition)
                        {
                            WordAccuracy[letterindex] = Correctness.InWord;
                        }

                        IncorrectPositions[letterindex, j] = true;
                    }

                    else if (gyn[j] == 'n')
                    {
                        WordAccuracy[letterindex] = Correctness.NotInWord;
                    }

                    else
                    {
                        throw new Exception("burn in hell");
                    }
                }
            }

            Console.ReadLine();
        }

        public static string GuessWord()
        {
            if (Guess == 1)
            {
                return "STARE";
            }

            return WordList.ProvideGuess();
        }
    }

    public enum Correctness
    {
        None,
        NotInWord,
        InWord,
        CorrectPosition
    }
}
