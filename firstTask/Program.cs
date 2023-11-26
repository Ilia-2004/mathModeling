using System;

namespace mathModeling;
internal abstract class Program
{ 
  // function
  private static double s_function(double x)
  {
    var numerator = Math.Pow(x - 1, 2);
    var denominator = x * x + 1;

    return Math.Pow(numerator, 1.0 / 3.0) / denominator;
  }
        
  // Svenn method
  private static (double, double) s_svennsMethod(double x)
  {
    var h = 0.5;
    var solution = s_function(x);

    Console.WriteLine("Решение уравнения: " + solution); 
      
    var a = x - h;
    var b = x + h;

    var fa = s_function(a);
    var fb = s_function(b);

    Console.WriteLine(fa + " " + solution + " " + fb);
    if (fa < solution && solution > fb) {
      Console.WriteLine("Отрезок, содержащий точку минимума: [" + a + ", " + b + "]");
      return (a, b);
    }
    else
    { 
      if (fa > solution && solution > fb)
      {
        h = -h;
        (a, b) = (b, a);
      }
    }

    while (fa > fb)
    {
      x += h;
      a = x - h;
      b = x + h;
      fa = s_function(a); fb = s_function(b);
    }

    if (h > 0) (a, b) = (b, a);

    Console.WriteLine("Отрезок, содержащий точку минимума: [" + a + ", " + b + "]");

    return (a, b);
  }

  // method of golden ratio  
  private static double s_FindMinimum(double x)
  {
    var result = s_svennsMethod(x);
    var a = result.Item1;
    var b = result.Item2;

    const double e = 0.001;
    var phi = (1 + Math.Sqrt(5)) / 2; 

    var x1 = a + 0.382 * (b - a);
    var x2 = b - 0.382 * (b - a);

    var f1 = s_function(x1);
    var f2 = s_function(x2);

    while (Math.Abs(b - a) > e)
    {
      if (f1 > f2)
      {
        b = x2;
        x2 = x1;
        f2 = f1;
        x1 = a + (1 - phi) * (b - a);
        f1 = s_function(x1);
      }
      else
      {
        a = x1;
        x1 = x2;
        f1 = f2;
        x2 = b - (1 - phi) * (b - a);
        f2 = s_function(x2);
      }
    }

    return (a + b) / 2;
  }

  /* Main mehtod */
  public static void Main(string[] args)
  {
    const double x = -1;
    Console.WriteLine(s_FindMinimum(x));
  }
}