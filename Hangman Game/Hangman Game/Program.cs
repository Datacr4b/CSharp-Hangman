using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangman_Game
{
    internal class Program
    {
        private static List<string> exposed_letters = new List<string>();
        static void Main(string[] args)
        {
            string input;
            string word = "hangman";

            while (true) // Main Game Loop
            {
                Console.Clear();
                // Display Word Method 
                DisplayWord(word);


                Console.WriteLine("Guess letter or word: ");
                input = Console.ReadLine();
                if (input.Trim().Length < 2 && input.Trim() != "")
                {
                    if (!exposed_letters.Contains(input.ToLower()))
                    {
                        GuessLetter(input, word);
                        if (CheckWinCon(word))
                            break;
                    }
                    else
                    { 
                        Console.WriteLine("Letter has already been revealed");
                    }
                }
                else if (input.Trim().Length > 1)
                {
                    if(GuessWord(input, word))
                    {
                        break;
                    }
                }
                else
                    Console.WriteLine("Invalid Input");
            }
            Console.Clear();
            DisplayWord(word);
            Console.WriteLine("CONGRATULATIONS!! ");
            Console.ReadLine();
        }

        static void DisplayWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (exposed_letters.Contains(word[i].ToString()))
                {
                    Console.Write(word[i]);
                }
                else
                    Console.Write("_");
            }
            Console.WriteLine();
        }

        static void GuessLetter(string letter, string word)
        {
            if (word.Contains(letter) && !exposed_letters.Contains(letter))
            {
                exposed_letters.Add(letter);
            }
        }

        static bool GuessWord(string guessWord, string word)
        {
            if (guessWord.ToLower() == word)
            {
                return true;
            }
            return false;
        }

        static bool CheckWinCon(string word)
        {
            foreach (char c in word)
            {
                if (!exposed_letters.Contains(c.ToString()))
                    return false;
            }
            return true;
        }

    }
}
