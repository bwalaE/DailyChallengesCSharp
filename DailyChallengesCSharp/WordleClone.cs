// a clone of Wordle in the C# console
//TODO: make the keyboard at the bottom to show remaining keys

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DailyChallengesCSharp
{
    internal class WordleClone
    {
        enum LetterStates
        {
            Black,
            Yellow,
            Green
        }
        public void ReMain()
        {
            Console.ForegroundColor = ConsoleColor.White;

            // the list of all possible words
            List<string> allWords = new List<string>();
            allWords = File.ReadAllLines(@"Resources/wordlist.txt").ToList(); // works

            // the (currently empty) list of guesses
            List<string> guesses = new List<string>();
            for (int i = 0; i < 6; i++) // populating the guesses (with empty strings)
            {
                guesses.Add("     ");
            }

            List<char> blacklist = new List<char>(); // the list of characters blacked out

            string answer; // change this later so it isn't defined here
            int turn = 0; // which turn the game is on
            string input;

            Random random = new Random();
            int randomPick = random.Next(allWords.Count); // range is 0 to 2314 (works correctly)
            answer = allWords[randomPick].ToUpper();
            //Console.WriteLine(allWords[randomPick]);

            Console.Clear();
            Console.WriteLine("Type 'q' to quit\n");
            printGuesses(guesses, answer, ref blacklist);
            printKeyboard(blacklist);

            //run game
            while (true)
            {
                Console.Write("guess: ");
                input = Console.ReadLine().ToUpper();
                //Console.WriteLine(input);

                if (input == "Q")
                {
                    Console.WriteLine("Quitting");
                    break;
                }

                if (input == "F")
                {
                    Console.WriteLine("You forfeit!");
                    Console.WriteLine("The word was " + answer);
                    break;
                }

                // handling invalid input
                if (!checkIfValidInput(input, ref allWords))
                {
                    continue;
                }
                else // input is valid, continue the game
                {
                    guesses[turn] = input;

                    Console.Clear();
                    printGuesses(guesses, answer, ref blacklist);
                    printKeyboard(blacklist);
                    turn++;
                    if (input == answer) // user won
                    {
                        Console.Clear();
                        printGuesses(guesses, answer, ref blacklist);
                        printKeyboard(blacklist);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("You won!");
                        //gameRunning = false;
                        break;
                    }
                    if (turn == 5) // user lost
                    {
                        Console.Clear();
                        guesses[5] = answer;
                        printGuesses(guesses, answer, ref blacklist);
                        printKeyboard(blacklist);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("You failed!");
                        //gameRunning = false;
                        break;
                    }
                }
            }
        }

        private void printGuesses(List<string> guesses, string answer, ref List<char> blacklist)
        {
            int letterState;

            foreach (string currentWord in guesses) // runs for each word in guesses
            {
                for (int i = 0; i < 5; i++) // runs for each char in currentWord
                {
                    letterState = checkStateOfLetter(answer, currentWord[i], i);
                    if(letterState == (int)LetterStates.Green) // if the letter is in the right position
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (letterState == (int)LetterStates.Yellow) // if the letter is somewhere else in the word
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else // if the letter is not in the word
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        blacklist.Add(currentWord[i]);
                    }
                    Console.Write("[" + currentWord[i] + "]");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int checkStateOfLetter(string answer, char c, int position)
        {
            int result = 0;

            foreach (char letter in answer) //check if c is in answer
            {
                if (letter == c)
                {
                    result = 1;
                    break;
                }
            }

            if (answer[position] == c) // check if c is at the right position in answer
            {
                result = 2;
            }

            return result;
        }

        private bool checkIfValidInput (string s, ref List<string> wordlist)
        {
            bool result = true;

            if (s.Length != 5)
            {
                Console.WriteLine("Word must be 5 letters!");
                result = false;
            }

            else if (!wordlist.Contains(s.ToLower()))
            {
                Console.WriteLine("Not in word list!");
                result = false;
            }

            return result;
        }

        private void printKeyboard(List<char> blacklist)
        {
            string keylist = "QWERTYUIOPASDFGHJKLZXCVBNM";

            foreach(char c in keylist)
            {
                if (blacklist.Contains(c)) // if the current letter is in the blacklist
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                if (c == 'A')
                {
                    Console.Write("\n ");
                }
                else if (c == 'Z')
                {
                    Console.Write("\n   ");
                }

                Console.Write("[" + c + "] ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
