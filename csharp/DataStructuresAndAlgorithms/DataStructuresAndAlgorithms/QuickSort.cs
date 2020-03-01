namespace DataStructuresAndAlgorithms
{
    public class QuickSort
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
            int pivotPosition = left;
            int pivot = arr[right];
            for (int i = left; i < right; i++)
            {
                if (arr[i] <= pivot)
                {
                    Swap(arr, pivotPosition, i);
                    pivotPosition++;
                }
            }
            Swap(arr, pivotPosition, right);

            return pivotPosition;
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
