using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresAndAlgorithms
{
    public class InsertionSort
    {
        /// <summary>
        /// Sort the given array by insertionsort.    
        /// </summary>
        /// <param name="arr">The array to be sorted.</param>
        public static void Sort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                Insert(arr, i - 1, arr[i]);
            }
        }
        /// <summary>
        /// Move elements of arr[0..rightIndex] that are greater than value and insert the given value to the right position.
        /// </summary>
        /// <param name="arr">The array in which insert opeartion will occur.</param>
        /// <param name="rightIndex">The end index of sub-array.</param>
        /// <param name="value">The value to be inserted in the right place.</param>
        static void Insert(int[] arr, int rightIndex, int value)
        {
            int i;
            for (i = rightIndex; i >= 0 && arr[i] > value; i--)
            {
                arr[i + 1] = arr[i];
            }
            arr[i + 1] = value;
        }
    }
}
