using System;

namespace fourTask;
internal abstract class Program
{
  /* Function method */
  private static float s_functionMethod(float x1, float x2) => (2 * x1) + (3 * x2);
    
  /* Main method */
  public static void Main()
  {
    const int n = 4;
    var a = new float[20, 2];
    var b = new float[20];
    float x1 = 0, x2 = 0;
    float max = -100;
      
    a[0, 0] = 8;  a[0, 1] = -5; b[0] = 11;
    a[1, 0] = -1; a[1, 1] = 3; b[1] = 1;
    a[2, 0] = 2;  a[2, 1] = 7; b[2] = 7;

    for (var i = 0; i < n; i++)
    { 
      for (var j = 0; j < n; j++)
      {
        if (!(Math.Abs(a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]) > 0.001)) continue;
        var x = (b[i] * a[j, 1] - a[i, 1] * b[j]) / (a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]);
        var y = (a[i, 0] * b[j] - b[i] * a[j, 0]) / (a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]);
        var t = 1;
        var result = false;
          
        Console.WriteLine($"{x} = {y} {i} {j}");
          
        for (var k = 0; k < n; k++)
        {
          switch (k)
          {
            case 1:
              if (a[k, 0] * x + a[k, 1] * y <= b[k]) t = 0;
              break;
            case 2:
              if (a[k, 0] * x + a[k, 1] * y <= b[k]) t = 0;
              break;
            case 3:
              if (a[k, 0] * x + a[k, 1] * y >= b[k]) t = 0;
              break;
          }
        }
          
        Console.WriteLine(t);

        if (t > 0) result = true;
        if ((!result) && ((!(s_functionMethod(x, y) > max)))) continue;
        max = s_functionMethod(x, y);
        x1 = x;
        x2 = y;
      }
    }
    Console.WriteLine(x1 + " " + x2 + " " + max);
  }
}