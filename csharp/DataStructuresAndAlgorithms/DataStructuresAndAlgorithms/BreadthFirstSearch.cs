using System.Collections.Generic;

namespace DataStructuresAndAlgorithms
{
    public class BreadthFirstSearch
    {
        /// <summary>
        /// Performs a breadth-first search on a graph
        /// </summary>
        /// <param name="graph">Graph, represented as adjacency arrays.</param>
        /// <param name="source">The index of the source vertex.</param>
        /// <returns>Array of tuples describing each vertex, like [(Distance: _, Predecessor: _ )]</returns>
        public static (int? Distance, int? Predecessor)[] DoBFS(int[][] graph, int source)
        {
            var bfsInfo = new (int? Distance, int? Predecessor)[graph.Length];

            //Initially set the distance and predecessor of each vertex to null
            for (int i = 0; i < graph.Length; i++)
            {
                bfsInfo[i] = (null, null);
            }

            // Assign source a distance of 0 and enqueue it to the queue.
            bfsInfo[source].Distance = 0;
            var queue = new Queue<int>();
            queue.Enqueue(source);

            // Traverse the graph as long as the queue is not empty.
            while (queue.Count != 0)
            {
                // Repeatedly dequeue a vertex u from the queue.
                var u = queue.Dequeue();
                for (int i = 0; i < graph[u].Length; i++)
                {
                    var v = graph[u][i];
                    // For each neighbor vertex v of vertex u that has not been visited:    
                    if (bfsInfo[v].Distance == null)
                    {
                        // Set distance to 1 greater than vertex u's distance
                        bfsInfo[v].Distance = bfsInfo[u].Distance + 1;
                        // Set predecessor to vertex u
                        bfsInfo[v].Predecessor = u;
                        // Enqueue vertex v
                        queue.Enqueue(v);
                    }
                }
            }
            return bfsInfo;
        }

    }
}




