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

        private static (double, double) _svennsMethod(double x)
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
                //solution = F(x);
                a = x - h;
                b = x + h;
                fa = F(a); fb = F(b);
            }

            if (h < 0) (a, b) = (b, a);

            Console.WriteLine("Отрезок, содержащий точку минимума: [" + a + ", " + b + "]");

            return (a, b);
        }

        public static double FindMinimum(double x)
        {
            var result = _svennsMethod(x);
            var a = result.Item1;
            var b = result.Item2;

            double e = 0.001;
            double phi = (Math.Sqrt(5) - 1) / 2; // Коэффициент золотого сечения

            double x1 = a + 0.382 * (b - a);
            double x2 = b - 0.382 * (b - a);

            double f1 = F(x1);
            double f2 = F(x2);

            while (Math.Abs(b - a) > e)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = a + phi * (b - a);
                    f1 = F(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = b - phi * (b - a);
                    f2 = F(x2);
                }
            }

            return (a + b) / 2;
        }

        public static void Main(string[] args)
        {
            double x = 0.5;

            Console.WriteLine(FindMinimum(x));
        }
    }
}