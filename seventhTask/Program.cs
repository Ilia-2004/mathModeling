using System;

namespace seventhTask
{
  internal class Program
  {
    private static int V = 9;

    /* Methods */


    /* Support methods */
    private static int s_maxDistance(int[] dist, bool[] sptSet)
    {
      // Initialize min value
      int min = int.MaxValue, min_index = -1;

       for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        /* Main method */
        static void Main()
    {
      /* Let us create the example 
      graph discussed above */
      var graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                               { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                               { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                               { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                               { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                               { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                               { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                               { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                               { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
      dijkstra(graph, 0);
    }
  }
}
