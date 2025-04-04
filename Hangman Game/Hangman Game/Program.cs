using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangman_Game
{
    internal class Program
    {
        private static int wrongGuesses = 0;
        private static List<char> exposed_letters = new List<char>();
        private static bool win = false;
        private static int limit;
        static void Main(string[] args)
        {

            string input;
            string word;
            List<string> words = new List<string>() {"strength", "minecraft", "america", "datacrab", "pixelart", "encyclopedia"};

            while (true) {
                Console.WriteLine("Pick a word or a random word(r) from our provided hangman word list!");
                input = Console.ReadLine();
                if (input.ToLower() == "r")
                {
                    Random rnd = new Random();
                    word = words[rnd.Next(0, words.Count)];
                    break;
                }
                else
                {
                    if (input.Trim() != "" && input.All(char.IsLetter))
                    {
                        word = input.Trim();
                        break;
                    }
                    else
                        Console.WriteLine("Invalid Input");

                }
            }

            limit = Convert.ToInt32(0.75 * word.Length);

            while (true) // Main Game Loop
            {
                // Check if won
                if (win)
                    break;
                // Display Word Method 
                DisplayWord(word);


                Console.WriteLine("Guess letter or word: ");
                input = Console.ReadLine();
                if (input.Trim().Length == 1 && char.IsLetter(input[0]))
                {
                    if (!exposed_letters.Contains(char.ToLower(input[0])))
                    {
                        GuessLetter(char.ToLower(input[0]), word);
                        // Check if lost
                        if (wrongGuesses > limit)
                            break;
                        win = CheckWinCon(word);
                    }
                    else
                    { 
                        Console.WriteLine("Letter has already been revealed");
                    }
                }
                else if (input.Trim().Length > 1)
                {
                    win = GuessWord(input, word);
                }
                else
                    Console.WriteLine("Invalid Input");
            }
            if (win)
            {
                DisplayWord(word);
                Console.WriteLine("Congratulations, you won! ");
                Console.ReadLine();
                
            }
            else
            {
                DisplayWord(word);
                Console.WriteLine("You lost!");
                Console.ReadLine();
            }
        }

        static void DisplayWord(string word)
        {
            Console.Clear();
            Console.WriteLine("Incorrect Guesses: " + wrongGuesses);
            Console.WriteLine("Guess Limit: " + limit);
            for (int i = 0; i < word.Length; i++)
            {
                if (exposed_letters.Contains(word[i]))
                {
                    Console.Write(word[i]);
                }
                else
                    Console.Write("_");
            }
            Console.WriteLine();
        }

        static void GuessLetter(char letter, string word)
        {
            if (word.Contains(letter) && !exposed_letters.Contains(letter))
            {
                exposed_letters.Add(letter);
            }
            else
            {
                wrongGuesses += 1;
                Console.WriteLine("Wrong guess!");
            }
        }

        static bool GuessWord(string guessWord, string word)
        {
            if (guessWord.ToLower() == word)
            {
                foreach (char c in word)
                {
                    if (!exposed_letters.Contains(c))
                        exposed_letters.Add(c);
                }
                return true;
            }
            return false;
        }

        static bool CheckWinCon(string word)
        {
            foreach (char c in word)
            {
                if (!exposed_letters.Contains(c))
                    return false;
            }
            return true;
        }

    }
}
