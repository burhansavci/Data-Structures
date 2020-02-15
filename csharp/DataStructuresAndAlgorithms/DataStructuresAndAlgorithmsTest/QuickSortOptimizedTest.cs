using DataStructuresAndAlgorithms;
using Xunit;

namespace DataStructuresAndAlgorithmsTest
{
    public class QuickSortOptimizedTest
    {
        [Fact]
        public void SortTest()
        {
            int[] arr1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            int[] arr2 = new int[] { 56, 34, 103, -1, 11, 0, -23, 10, 75, 103 };
            int[] arr3 = new int[] { -12, -15, -6, -3, -2, 0, -23, -13, -75 };
            QuickSortOptimized.Sort(arr1);
            QuickSortOptimized.Sort(arr2);
            QuickSortOptimized.Sort(arr3);
            Assert.Equal(arr1, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.Equal(arr2, new int[] { -23, -1, 0, 10, 11, 34, 56, 75, 103, 103 });
            Assert.Equal(arr3, new int[] { -75, -23, -15, -13, -12, -6, -3, -2, 0 });
        }
    }
}