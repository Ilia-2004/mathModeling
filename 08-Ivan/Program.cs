using System;
class Knapsack
{
  static void Main()
  {
    int[] u = {21, 64, 45, 73, 87, 86, 91, 64, 44, 85}; // Полезность
    int[] v = { 8,  1,  8,  4,  5,  10,  6,  9,  3,  8}; // Вес
    int M = 16; // Максимальный вес
    int n = u.Length;
    int[,] dp = new int[n + 1, M + 1];
    // Инициализация первой строки и столбца нулями
    for (int i = 0; i <= n; i++)
      dp[i, 0] = 0;
    for (int i = 0; i <= M; i++)
      dp[0, i] = 0;
    // Заполнение таблицы dp
    for (int i = 1; i <= n; i++)
    {
      for (int j = 1; j <= M; j++)
      {
        if (v[i - 1] <= j)
          dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - v[i - 1]] + u[i - 1]);
        else
          dp[i, j] = dp[i - 1, j];
      }
    }
    // Восстановление ответа
    int res = dp[n, M];
    int w = M;
    string itemsIncluded = "";
    for (int i = n; i > 0 && res > 0; i--)
    {
      if (res != dp[i - 1, w])
      {
        itemsIncluded = i + " " + itemsIncluded; // Добавление номера предмета
        res -= u[i - 1];
        w -= v[i - 1];
      }
    }
    for (int i = 0; i < n + 1; i++)
    {
      for (int j = 0; j < M + 1; j++)
      {
        Console.Write(dp[i,j]+" ");
      }
      Console.WriteLine();
    }
    Console.WriteLine("Максимальная полезность: " + dp[n, M]);
    Console.WriteLine("Предметы в рюкзаке: " + itemsIncluded);
  }
}