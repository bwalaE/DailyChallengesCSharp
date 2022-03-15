// LeetCode 413. Arithmetic Slices

using System;
using System.Collections.Generic;
using System.Text;

namespace DailyChallengesCSharp
{
    internal class LC413
    {
        public int ReMain(int[] nums)
        {
            int validSubarraysResult;

            int subarrayLength;
            int numOfPossibleSubarrayLengths;
            int subarraysOfCurrentLength;
            int startPos;
            //int[] inputArray = { 1, 2, 3, 4, 5 };
            List<int> currentSubarrayList = new List<int>();
            List<int[]> subarrayList = new List<int[]>();
            List<int[]> arithmeticSubarrayList = new List<int[]>();

            validSubarraysResult = 0;

            numOfPossibleSubarrayLengths = 0;
            subarraysOfCurrentLength = 0;


            foreach (int num in nums)
            {
                if (num < -1000 | num > 1000)
                {
                    Console.WriteLine("Invalid int size, each int must be between -1000 and 1000");
                    return -1;
                }
            }

            //exception handling
            if (nums.Length < 1 | nums.Length > 5000 )
            {
                Console.WriteLine("Invalid array size, array must be of a size from 1 to 5000");
                return -1;
            }
            else
            {
                numOfPossibleSubarrayLengths = nums.Length - 2;
                // if Arraylength is 3, NOPSL is 1. if AL is 4, NOPSL is 2. if AL = 5, NOPSL = 3, and so on
                subarrayLength = 3;

                for (int i = 0; i < numOfPossibleSubarrayLengths; i++) // this runs for each possible subarray length
                {
                    startPos = 0;
                    subarraysOfCurrentLength = findNumOfArraysOfLength(nums.Length, subarrayLength);
                    for (int j = 0; j < subarraysOfCurrentLength; j++) // runs for each subarray of current length
                    {
                        currentSubarrayList.Clear();
                        for (int k = 0; k < subarrayLength; k++) // runs for each element to be added to currentList
                        {
                            currentSubarrayList.Add(nums[k + startPos]);
                        }
                        subarrayList.Add(currentSubarrayList.ToArray());
                        validSubarraysResult++;
                        startPos++;
                    }
                    subarrayLength++;
                }
            }

            arithmeticSubarrayList = findAllArithmeticArrays(subarrayList);
            //printListOfArrays(arithmeticSubarrayList);
            return arithmeticSubarrayList.Count;
            
        }

        // this method was tested and it works
        private int findNumOfArraysOfLength (int arrayLength, int subarrayLength)
        {
            int result = 0;
            int[] array = new int[arrayLength];

            for (int i = 0;i < arrayLength;i++)
            {
                if (subarrayLength + i > arrayLength)
                {
                    break;
                }
                else
                {
                    result++;
                }
            }

            return result;
        }

        private void printListOfArrays (List<int[]> list)
        {
            foreach (int[] array in list)
            {
                foreach (int item in array)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
        }

        private List<int[]> findAllArithmeticArrays (List<int[]> list) // to filter out all non-arithmetic arrays
        {
            List<int[]> result = new List<int[]>();
            //result = list;

            int constant;
            int currentArrLength;

            bool flag;

            for (int i = 0; i < list.Count; i++) // for each array in the list
            {
                flag = true;

                currentArrLength = list[i].Length;
                constant = list[i][currentArrLength-1] - list[i][currentArrLength-2];
                /*Console.WriteLine("current array: " + i + ". size: " + list[i].Length
                    + ". last element: " + list[i][currentArrLength - 1]
                    + ". second last element: " + list[i][currentArrLength - 2]
                    + ". third last element: " + list[i][currentArrLength - 3]);*/

                for (int j = list[i].Length-1; j > 0; j--) // for each in the current array
                {
                    if (list[i][j] - list[i][j - 1] != constant)
                    {
                        //Console.WriteLine("array " + i + " removed");
                        flag = false;
                    }
                    
                    
                }

                if (flag)
                {
                    result.Add(list[i]);
                }
                
            }

            //Console.WriteLine(list[0][1]);

            return result;
        }
    }
}
