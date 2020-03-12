using DataStructuresAndAlgorithms;
using Xunit;

namespace DataStructuresAndAlgorithmsTest
{
    public class TestEnvironment
    {
        [Fact]
        public void QuickSortTest()
        {
            int[] arr1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            int[] arr2 = new int[] { 56, 34, 103, -1, 11, 0, -23, 10, 75, 103 };
            int[] arr3 = new int[] { -12, -15, -6, -3, -2, 0, -23, -13, -75 };
            QuickSort.Sort(arr1);
            QuickSort.Sort(arr2);
            QuickSort.Sort(arr3);
            Assert.Equal(arr1, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.Equal(arr2, new int[] { -23, -1, 0, 10, 11, 34, 56, 75, 103, 103 });
            Assert.Equal(arr3, new int[] { -75, -23, -15, -13, -12, -6, -3, -2, 0 });
        }
        [Fact]
        public void QuickSortOptimizedTest()
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
        [Fact]
        public void SelectionSortTest()
        {
            int[] arr1 = new int[] { 3, 4, 5, 1, 2, -23, -10 };
            int[] arr2 = new int[] { 9, 2, 3, 2, 4, 3, -10, 55, -10, 0 };
            int[] arr3 = new int[] { 100, 99, 5, 4, 3, 2, 1, 0, -10, -78 };
            SelectionSort.Sort(arr1);
            SelectionSort.Sort(arr2);
            SelectionSort.Sort(arr3);
            Assert.Equal(arr1, new int[] { -23, -10, 1, 2, 3, 4, 5 });
            Assert.Equal(arr2, new int[] { -10, -10, 0, 2, 2, 3, 3, 4, 9, 55 });
            Assert.Equal(arr3, new int[] { -78, -10, 0, 1, 2, 3, 4, 5, 99, 100 });
        }
        [Fact]
        public void InsertionSortTest()
        {
            int[] arr1 = new int[] { 3, 4, 5, 1, 2, -23, -10 };
            int[] arr2 = new int[] { 9, 2, 3, 2, 4, 3, -10, 55, -10, 0 };
            int[] arr3 = new int[] { 100, 99, 5, 4, 3, 2, 1, 0, -10, -78 };
            InsertionSort.Sort(arr1);
            InsertionSort.Sort(arr2);
            InsertionSort.Sort(arr3);
            Assert.Equal(arr1, new int[] { -23, -10, 1, 2, 3, 4, 5 });
            Assert.Equal(arr2, new int[] { -10, -10, 0, 2, 2, 3, 3, 4, 9, 55 });
            Assert.Equal(arr3, new int[] { -78, -10, 0, 1, 2, 3, 4, 5, 99, 100 });
        }
        [Fact]
        public void MergeSortTest()
        {
            int[] arr1 = new int[] { 3, 4, 5, 1, 2, -23, -10 };
            int[] arr2 = new int[] { 9, 2, 3, 2, 4, 3, -10, 55, -10, 0 };
            int[] arr3 = new int[] { 100, 99, 5, 4, 3, 2, 1, 0, -10, -78 };
            MergeSort.Sort(arr1);
            MergeSort.Sort(arr2);
            MergeSort.Sort(arr3);
            Assert.Equal(arr1, new int[] { -23, -10, 1, 2, 3, 4, 5 });
            Assert.Equal(arr2, new int[] { -10, -10, 0, 2, 2, 3, 3, 4, 9, 55 });
            Assert.Equal(arr3, new int[] { -78, -10, 0, 1, 2, 3, 4, 5, 99, 100 });
        }
        [Fact]
        public void BreadthFirstSearchTest()
        {
            int[][] adjacencyArray = new int[][] {
                new int[] { 1 },
                new int[] { 0, 4, 5 },
                new int[] { 3, 4, 5 },
                new int[] { 2, 6 },
                new int[] { 1, 2 },
                new int[] { 1, 2, 6 },
                new int[] { 3, 5 },
                new int[]{}
            };
            var bfsInfo = BreadthFirstSearch.DoBFS(adjacencyArray, 3);
            Assert.Equal(bfsInfo[0], (Distance: 4, Predecessor: 1));
            Assert.Equal(bfsInfo[1], (Distance: 3, Predecessor: 4));
            Assert.Equal(bfsInfo[2], (Distance: 1, Predecessor: 3));
            Assert.Equal(bfsInfo[3], (Distance: 0, Predecessor: null));
            Assert.Equal(bfsInfo[4], (Distance: 2, Predecessor: 2));
            Assert.Equal(bfsInfo[5], (Distance: 2, Predecessor: 2));
            Assert.Equal(bfsInfo[6], (Distance: 1, Predecessor: 3));
            Assert.Equal(bfsInfo[7], (Distance: null, Predecessor: null));
        }
    }
}
