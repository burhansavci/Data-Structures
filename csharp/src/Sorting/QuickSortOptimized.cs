using System;

namespace Sorting
{
    public class QuickSortOptimized
    {
        /// <summary>
        /// Sort the given array by quicksort.    
        /// </summary>
        /// <param name="arr">The array to be sorted</param>
        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }
        /// <summary>
        /// Recursively find the pivot and sort the given array, dividing each array as it recurses.
        /// </summary>
        /// <param name="arr">The array to be sorted.</param>
        /// <param name="left">The starting point of the sorting.</param>
        /// <param name="right">The end point of the sorting.</param>
        static void Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                Sort(arr, left, pivot - 1);
                Sort(arr, pivot + 1, right);
            }
        }
        /// <summary>
        /// Partitions the given array and returns the index of the pivot.
        /// </summary>
        /// <param name="arr">The array to be partitioned.</param>
        /// <param name="left">The starting point of the partitioning.</param>
        /// <param name="right">The end point of the partitioning.</param>
        /// <returns>index of the pivot</returns>
        static int Partition(int[] arr, int left, int right)
        {
            int pivotPosition = GetPivotPosition(left, right);
            Swap(arr, pivotPosition, right);

            int pivot = arr[right];
            int newPivotPosition = left;

            for (int i = left; i < right; i++)
            {
                if (arr[i] <= pivot)
                {
                    Swap(arr, newPivotPosition, i);
                    newPivotPosition++;
                }
            }

            Swap(arr, newPivotPosition, right);

            return newPivotPosition;
        }
        /// <summary>
        ///  Gets the random pivot position with the specified range.
        /// </summary>
        /// <param name="left">The start point of specified the range.</param>
        /// <param name="right">The end point of the specified range.</</param>
        /// <returns>Random pivot position</returns>
        static int GetPivotPosition(int left, int right)
        {
            var random = new Random();
            return Median(random.Next(left, right), random.Next(left, right), random.Next(left, right));
        }
        /// <summary>
        /// Finds the median of three and returns it
        /// </summary>
        /// <param name="a">The number to be compared with the others.</param>
        /// <param name="b">The number to be compared with the others</param>
        /// <param name="c">The number to be compared with the others</param>
        /// <returns>Median of three</returns>
        static int Median(int a, int b, int c)
        {
            if (c < a)
            {
                if (a < b) return a; //c<a and a<b then c<a<b
                else if (c < b) return b; //c<b and b<a then c<b<a
                else return c; //b<c and c<a then b<c<a
            }
            else
            {
                if (b < a) return a; //b<a and a<c then b<a<c
                else if (b < c) return b; //a<b and b<c then a<b<c
                else return c; //a<c and c<b then a<c<b
            }
        }
        /// <summary>
        ///Swaps two items in an array
        /// </summary>
        /// <param name="arr">The array where the swap will occur.</param>
        /// <param name="firstIndex">The first index to be used in swap process.</param>
        /// <param name="secondIndex">The seconf index to be used in swap process.</param>
        static void Swap(int[] arr, int firstIndex, int secondIndex)
        {
            int temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }
    }
}
