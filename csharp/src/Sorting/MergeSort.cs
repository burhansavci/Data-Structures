namespace Sorting
{
    public class MergeSort
    {
        /// <summary>
        /// Sort the given array by mergesort.
        /// </summary>
        /// <param name="arr">The array to be sorted.</param>
        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }
        /// <summary>
        /// Recursively calculate the middle pivot and sort the given array, merging each array as it recurses.
        /// </summary>
        /// <param name="arr">The array to be sorted.</param>
        /// <param name="lowHalfStart">The start index of lower half of the given array.</param>
        /// <param name="highHalfEnd">The end index of higher half of the given array.</param>
        static void Sort(int[] arr, int lowHalfStart, int highHalfEnd)
        {
            if (lowHalfStart < highHalfEnd)
            {
                int pivot = (lowHalfStart + highHalfEnd) / 2;
                Sort(arr, lowHalfStart, pivot);
                Sort(arr, pivot + 1, highHalfEnd);
                Merge(arr, lowHalfStart, pivot, highHalfEnd);
            }
        }
        /// <summary>
        /// Takes in an array that has two sorted subarrays, from [lowHalfStart..pivot] and [pivot+1..highHalfEnd], and merges the array.
        /// </summary>
        /// <param name="arr">The array to be merged.</param>
        /// <param name="lowHalfStart">The start index of lower half of the given array.</param>
        /// <param name="pivot">The middle index of the given array.</param>
        /// <param name="highHalfEnd">The end index of higher half of the given array.</param>
        static void Merge(int[] arr, int lowHalfStart, int pivot, int highHalfEnd)
        {
            int[] lowHalf = new int[pivot - lowHalfStart + 1];
            int[] highHalf = new int[highHalfEnd - pivot];

            int arrPointer = lowHalfStart;
            //Copy the given array into lowHalf and highHalf. 
            for (int i = 0; arrPointer <= pivot; i++, arrPointer++)
            {
                lowHalf[i] = arr[arrPointer];
            }
            for (int i = 0; arrPointer <= highHalfEnd; i++, arrPointer++)
            {
                highHalf[i] = arr[arrPointer];
            }

            arrPointer = lowHalfStart;
            int lowHalfPointer = 0;
            int highHalfPointer = 0;
            // Repeatedly compare the lowest untaken element in
            // lowHalf with the lowest untaken element in highHalf
            // and copy the lower of the two back into arr.
            while (lowHalfPointer < lowHalf.Length && highHalfPointer < highHalf.Length)
            {
                if (lowHalf[lowHalfPointer] < highHalf[highHalfPointer])
                {
                    arr[arrPointer] = lowHalf[lowHalfPointer];
                    lowHalfPointer++;
                }
                else
                {
                    arr[arrPointer] = highHalf[highHalfPointer];
                    highHalfPointer++;
                }
                arrPointer++;
            }

            // Once one of lowHalf and highHalf has been fully copied
            // back into arr, copy the remaining elements from the
            // other temporary array back into the arr.
            while (lowHalfPointer < lowHalf.Length)
            {
                arr[arrPointer] = lowHalf[lowHalfPointer];
                lowHalfPointer++;
                arrPointer++;
            }
            while (highHalfPointer < highHalf.Length)
            {
                arr[arrPointer] = highHalf[highHalfPointer];
                highHalfPointer++;
                arrPointer++;
            }
        }
    }
}
