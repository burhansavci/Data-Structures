using Xunit;

namespace Search
{
    public class SearchTest
    {
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