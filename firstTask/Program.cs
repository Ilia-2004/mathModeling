using System;

namespace mathModeling
{
  internal abstract class Program
  {
    private static double F(double x)
    {
      var numerator = Math.Pow(x - 1, 2);
      var denominator = x * x + 1;

      return Math.Pow(numerator / denominator, 1.0 / 3.0);
    }

    public (double, double) SvennsMethod(double x)
    {
      var h = 0.5;
      var solution = F(x);

      Console.WriteLine("Решение уравнения: " + solution);

      // Применение метода Свенна для поиска отрезка, содержащего точку минимума
      var a = x - h; // Левая граница интервала
      var b = x + h; // Правая граница интервала

      var fa = F(a);
      var fb = F(b);

      if (fa > solution && solution < fb) return (a, b);
      else
      {
        if (fa < solution && solution < fb)
        {
          h = -h;
          (a, b) = (b, a);
        }
      }

      while (fa < fb)
      { 
        x += h;
        solution = F(x);
        a = x - h;
        b = x + h;
        fa = F(a); fb = F(b);
      }

      if (h < 0) (a, b) = (b, a);

      Console.WriteLine("Отрезок, содержащий точку минимума: [" + a + ", " + b + "]");

      return (a, b);
    }
    
    public static void Main(string[] args)
    {
    }
  }
}