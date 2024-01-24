using System;
using System.Collections.Generic;

internal abstract class Program
{
  /* Constant's */
  private const int V = 13; // Количество вершин в графе

  #region SupportMethods
  // Метод для нахождения вершины с максимальным расстоянием
  private static int s_maxDistanceMethod(IReadOnlyList<int> dist, IReadOnlyList<bool> sptSet)
  {
    // Инициализация максимального значения и индекса
    int max = int.MinValue, maxIndex = -1;

    for (var v = 0; v < V; v++)
    {
      if (sptSet[v] || dist[v] < max) continue;
      max = dist[v];
      maxIndex = v;
    }

    return maxIndex;
  }

  // Метод для вывода результатов
  private static void s_printSolution(IReadOnlyList<int> dist, List<int> path)
  {
    Console.WriteLine("Вершина Расстояние от источника");
    for (var i = 0; i < V; i++)
      Console.WriteLine(i + "\t\t" + dist[i]);

    Console.WriteLine("\nВершины в пути с максимальной стоимостью:");
    foreach (var vertex in path)
      Console.WriteLine("B" + (vertex + 1));
  }
  #endregion
  
  /* Метод Дейкстры */
  private static void s_dijkstraMethod(int[,] graph, int src)
  {
    var dist = new int[V]; // Массив для хранения расстояний от источника до вершин
    var sptSet = new bool[V]; // Массив для отметки вершин, уже включенных в кратчайший путь

    // Инициализация массивов
    for (var i = 0; i < V; i++)
    {
      dist[i] = int.MinValue;
      sptSet[i] = false;
    }

    // Расстояние от источника до самого себя всегда равно 0
    dist[src] = 0;

    // Нахождение кратчайших путей для всех вершин
    for (var count = 0; count < V - 1; count++)
    {
      // Выбор вершины с максимальным расстоянием
      var u = s_maxDistanceMethod(dist, sptSet);

      // Отметка выбранной вершины как обработанной
      sptSet[u] = true;

      // Обновление расстояний для смежных вершин
      for (var v = 0; v < V; v++)
      {
        if (!sptSet[v] && graph[u, v] != 0 &&
            dist[u] != int.MinValue && dist[u] + graph[u, v] > dist[v])
          dist[v] = dist[u] + graph[u, v];
      }
    }

    // Сохранение вершин в пути с максимальной стоимостью
    var path = new List<int>();
    for (var i = 0; i < V; i++)
    {
      if (dist[i] != int.MinValue)
        path.Add(i);
    }

    // Вывод массива расстояний и вершин в пути
    s_printSolution(dist, path);
  }

  /* Главный метод */
  public static void Main()
  {
    /* Создание примера графа */
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

    // Нахождение кратчайшего пути от вершины 1 до вершины 10
    Console.WriteLine("Кратчайший путь от вершины 1 до вершины 10:");
    s_dijkstraMethod(graph, 0);

    // Нахождение кратчайшего пути от вершины 2 до вершины 10
    Console.WriteLine("\nКратчайший путь от вершины 2 до вершины 10:");
    s_dijkstraMethod(graph, 1); // В данном случае источник - вершина 2
  }
}
