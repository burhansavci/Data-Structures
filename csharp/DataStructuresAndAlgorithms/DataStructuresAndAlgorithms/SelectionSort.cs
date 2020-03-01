using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresAndAlgorithms
{
    public class SelectionSort
    {
        /// <summary>
        /// Sort the given array by selectionsort.    
        /// </summary>
        /// <param name="arr">The array to be sorted</param>
        public static void Sort(int[] arr)
        {
            int minIndex;
            for (int i = 0; i < arr.Length; i++)
            {
                minIndex = IndexOfMinumum(arr, i);
                Swap(arr, minIndex, i);
            }
        }
        /// <summary>
        /// Find the index of the minimum number by looping through the sub-array created from the start index.
        /// </summary>
        /// <param name="arr">The array in which the loop will take place.</param>
        /// <param name="startIndex">The start index of the sub-array.</param>
        /// <returns>The index of the minimum number.</returns>
        static int IndexOfMinumum(int[] arr, int startIndex)
        {
            int minValue = arr[startIndex];
            int minIndex = startIndex;

            for (int i = minIndex + 1; i < arr.Length; i++)
            {
                if (arr[i] < minValue)
                {
                    minValue = arr[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
        /// <summary>
        ///Swaps two items in an array
        /// </summary>
        /// <param name="arr">The array where the swap will occur.</param>
        /// <param name="firstIndex">The first index to be used in swap process.</param>
        /// <param name="secondIndex">The second index to be used in swap process.</param>
        static void Swap(int[] arr, int firstIndex, int secondIndex)
        {
            int temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }
    }
}
