using System;
using System.Collections.Generic;
using System.Text;

namespace Challenges
{
    public class QuickSort
    {
        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }
        static void Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                Sort(arr, left, pivot - 1);
                Sort(arr, pivot + 1, right);
            }
        }
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
        static void Swap(int[] arr, int firstIndex, int secondIndex)
        {
            int temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }
    }
}
