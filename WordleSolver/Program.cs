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
            Console.WriteLine("Wordle Solver\n");
            
            Console.WriteLine("Instructions for Use");
            Console.WriteLine(" 1. Open Wordle.");
            Console.WriteLine(" 2. Input generated guess into Wordle.");
            Console.WriteLine(" 3. Input results like so\n   - for each green tile, enter \"g\"\n   - for each yellow tile, enter \"y\"\n   - for each grey tile, enter \"n\".");
            Console.WriteLine(" 4. Repeat steps 2-3 until you win or run out of attempts.");


            for (int i = 0; i < 6; i++)
            {
                Guess++;
                string word = GuessWord();
                Console.WriteLine();
                Console.WriteLine($"Guess: {word.ToUpperInvariant()}");

                string[] gynlist = new string[1];
                string gyn;
                bool repeat = true;
                while (repeat)
                {
                    Console.Write("Input results of guess: ");
                    gynlist[0] = Console.ReadLine();
                    repeat = false;
                    if (gynlist[0].Length != 5)
                    {
                        Console.WriteLine("Wrong length of results. Please try again.");
                        repeat = true;
                    }

                    foreach (char gin in gynlist[0])
                    {
                        if (gin != 'g' && gin != 'y' && gin != 'n')
                        {
                            Console.WriteLine("That seems like a misplaced letter there. Try again.");
                            repeat = true;
                            break;
                        }
                    }
                }

                gyn = gynlist[0];
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
