using System;

namespace fourTask
{
  internal abstract class Program
  {
    private static float F(float x, float y) => x + 3 * y;
    
    public static void Main(string[] args)
    {
      const int n = 4;
      var a = new float[20, 2];
      var b = new float[20];
      float x, y, x1 = 0, x2 = 0; 
      float max = -100;
      
      a[0, 0] = 1; a[0, 1] = 4; b[0] = 4;
      a[1, 0] = 1; a[1, 1] = 1; b[1] = 6;
      a[2, 0] = 1; a[2, 1] = 0; b[2] = 2;
      a[3, 0] = 0; a[3, 1] = 1; b[3] = 0;

      for (var i = 0; i < n; i++)
      { 
        for (var j = 0; j < n; j++)
        {
          if (Math.Abs(a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]) > 0.001)
          {
            x = (b[i] * a[j, 1] - a[i, 1] * b[j]) / (a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]);
            y = (a[i, 0] * b[j] - b[i] * a[j, 0]) / (a[i, 0] * a[j, 1] - a[i, 1] * a[j, 0]);
            var t = 1;
            var result = false;
            Console.WriteLine($"{x} = {y} {i} {j}");
            for (var k = 0; k < n; k++)
            {
              switch (k)
              {
                case 1:
                  if (a[k, 0] * x + a[k, 1] * y < b[k]) t = 0;
                  break;
                case 2:
                  if (a[k, 0] * x + a[k, 1] * y > b[k]) t = 0;
                  break;
                case 3:
                  if (a[k, 0] * x + a[k, 1] * y < b[k]) t = 0;
                  break;
                case 4:
                  if (a[k, 0] * x + a[k, 1] * y < b[k]) t = 0;
                  break;
              }
            }
            Console.WriteLine(t);

            if (t > 0) result = true;
            if ((result) || ((F(x, y) > max)))
            {
              max = F(x, y);
              x1 = x;
              x2 = y;
            }
          }
        }
      }
      Console.WriteLine(x1 + " " + x2 + " " + max);
    }
  }
}