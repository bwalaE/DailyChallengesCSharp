using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DailyChallengesCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordleClone wc = new WordleClone();
            wc.ReMain();

        }
        public void C5_Intermediate()
        {
            //desired results: entering oecd should return 3, edmo 2, rekt 1, and zyxo 0

            string inputWord;
            int anagrams = 0;

            List<string> wordList = new List<string>();
            string filepath = @"C:\Users\Jack\Desktop\test\test2.txt";
            wordList = File.ReadAllLines(filepath).ToList();
            //the above code was tested and it works

            Console.WriteLine("The list:\n");

            foreach (String line in wordList)
            {
                Console.WriteLine(line);
            }

            Console.Write("\nEnter a string to search the list for: ");
            inputWord = Console.ReadLine();

            foreach (String line in wordList) // runs for each word in the list
            {
                if (String.Concat(inputWord.OrderBy(c => c)) == String.Concat(line.OrderBy(c => c)))
                {
                    anagrams++;
                }
            }

            Console.WriteLine("Number of anagrams: " + anagrams);

        }
        public bool lc413Test(int[] arr) // check whether an array is arithmetic
        {
            bool result = true;

            int constant;

            constant = arr[arr.Length-1] - arr[arr.Length-2]; // -1 is the last element and -2 is the second-last
            //Console.WriteLine(arr[arr.Length-1] + " : " + arr[arr.Length - 2]);

            for (int i = arr.Length-1; i > 0; i--) // starts at the end of the array and goes back
            {
                if (arr[i] - arr[i-1] != constant)
                {
                    result = false;
                }
            }

            return result;

        }
    }
}
