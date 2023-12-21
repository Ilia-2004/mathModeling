using System;

namespace eighthTask;
internal abstract class Program
{
  /* Main method */
  public static void Main()
  {
    const int k = 10, i1 = 22;
    var d = new int[k + 1, i1 + 1];
    
    int[] w = { 0, 9, 7, 6, 10, 1, 3, 9, 10, 4, 2 };
    int[] s = { 0, 52, 78, 37, 95, 22, 37, 89, 45, 52, 97 };

    for (var i = 1; i <= k; i++)
      for (var j = 1; j <= i1; j++)
      {
        d[i, j] = Math.Max(d[i, j - 1], d[i - 1, j]);

        if ((j >= w[i]) && (d[i - 1, j - w[i]] + s[i] > d[i, j]))
          d[i, j] = d[i - 1, j - w[i]] + s[i];
      }

    for (var i = 0; i <= k; i++)
    {
      Console.WriteLine();
      for (var j = 0; j <= i1; j++)
        Console.WriteLine(d[i, j]);
    }

    var i2 = k;
    var j1 = i1; 
    Console.WriteLine();
    while (i2 != 0 && j1 != 0)
    {
      while (d[i2, j1] == d[i2 - 1, j1])
        i2--;

      if (d[i2, j1] != d[i2 - 1, j1 - w[i2]] + s[i2]) continue;
      Console.WriteLine();
      j1 = j1 - w[i2];
      i2 = i2 - 1;
    }
  }
}