using System;
using System.Collections.Generic;

namespace seventhTask
{
  internal abstract class Program
  {
    private const int V = 9;

    /* Support methods */
    // find the max distance
    private static int s_maxDistance(IReadOnlyList<int> dist, IReadOnlyList<bool> sptSet)
    {
      // Initialize max value
      int max = int.MinValue, maxIndex = -1;

      for (var v = 0; v < V; v++)
      {
        if (sptSet[v] || dist[v] < max) continue;
        max = dist[v];
        maxIndex = v;
      }

      return maxIndex;
    }

    // print solution
    private static void s_printSolution(IReadOnlyList<int> dist, List<int> path)
    {
      Console.WriteLine("Vertex Distance from Source");
      for (var i = 0; i < V; i++)
        Console.WriteLine(i + "\t\t" + dist[i]);

      Console.WriteLine("\nVertices in the path with maximum cost:");
      foreach (var vertex in path)
        Console.WriteLine("B" + (vertex + 1));
    }

    /* Methods */
    // the method of dijkstra
    private static void s_dijkstra(int[,] graph, int src)
    {
      var dist = new int[V]; // The output array. dist[i]
      // will hold the maximum
      // distance from src to i

      // sptSet[i] will true if vertex
      // i is included in longest path
      // tree or longest distance from
      // src to i is finalized
      var sptSet = new bool[V];

      // Initialize all distances as
      // MIN_VALUE and stpSet[] as false
      for (var i = 0; i < V; i++)
      {
        dist[i] = int.MinValue;
        sptSet[i] = false;
      }

      // Distance of source vertex
      // from itself is always 0
      dist[src] = 0;

      // Find longest path for all vertices
      for (var count = 0; count < V - 1; count++)
      {
        // Pick the maximum distance vertex
        // from the set of vertices not yet
        // processed. u is always equal to
        // src in the first iteration.
        var u = s_maxDistance(dist, sptSet);

        // Mark the picked vertex as processed
        sptSet[u] = true;

        // Update dist value of the adjacent
        // vertices of the picked vertex.
        for (var v = 0; v < V; v++)
        {
          // Update dist[v] only if it is not in
          // sptSet, there is an edge from u
          // to v, and the total weight of the path
          // from src to v through u is greater
          // than the current value of dist[v]
          if (!sptSet[v] && graph[u, v] != 0 &&
            dist[u] != int.MinValue && dist[u] + graph[u, v] > dist[v])
            dist[v] = dist[u] + graph[u, v];
        }
      }

      // Store the vertices in the path with maximum cost
      var path = new List<int>();
      for (var i = 0; i < V; i++)
      {
        if (dist[i] != int.MinValue)
          path.Add(i);
      }

      // print the constructed distance array and the vertices in the path
      s_printSolution(dist, path);
    }

    /* Main method */
    private static void Main()
    {
      /* Let us create the example
      graph discussed above */
      var graph = new[,]
      {
        {0, 4, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {4, 0, 0, 6, 10, 8, 0, 0, 0, 0, 0, 0, 0},
        {6, 0, 0, 0, 9, 11, 6, 0, 0, 0, 0, 0, 0},
        {0, 6, 0, 0, 0, 0, 0, 9, 10, 0, 0, 0, 0},
        {0, 10, 9, 0, 0, 0, 0, 11, 16, 0, 0, 0, 0},
        {0, 8, 11, 0, 0, 0, 0, 8, 18, 2, 0, 0, 0},
        {0, 0, 6, 0, 0, 0, 0, 8, 8, 0, 0, 0, 0},
        {0, 0, 0, 9, 11, 0, 0, 0, 0, 0, 7, 8, 0},
        {0, 0, 0, 10, 16, 8, 0, 0, 0, 0, 4, 5, 0},
        {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 7, 11, 0},
        {0, 0, 0, 0, 0, 0, 0, 7, 4, 7, 0, 0, 7},
        {0, 0, 0, 0, 0, 0, 0, 8, 5, 11, 0, 0, 8},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 8, 0}
      };

      s_dijkstra(graph, 0);
    }
  }
}
