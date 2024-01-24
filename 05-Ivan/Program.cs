using System;
using System.Collections.Generic;
namespace Lab5
{
    internal class Program
    {
        public class Simplex
        {
            //source - симплекс таблица без базисных переменных
            double[,] table; //симплекс таблица
            int m, n;
            List<int> basis; //список базисных переменных
            public Simplex(double[,] source)
            {
                m = source.GetLength(0);
                n = source.GetLength(1);
                table = new double[m, n + m - 1];
                basis = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if (j < n)
                            table[i, j] = source[i, j];
                        else
                            table[i, j] = 0;
                    }
                    //выставляем коэффициент 1 перед базисной переменной в строке
                    if ((n + i) < table.GetLength(1))
                    {
                        table[i, n + i] = 1;
                        basis.Add(n + i);
                    }
                }
                n = table.GetLength(1);
            }
            //result - в этот массив будут записаны полученные значения X
            public double[,] Calculate(double[] result)
            {
                int mainCol, mainRow; //ведущие столбец и строка
                while (!IsItEnd())
                {
                    mainCol = findMainCol();
                    mainRow = findMainRow(mainCol);
                    basis[mainRow] = mainCol;
                    double[,] new_table = new double[m, n];
                    for (int j = 0; j < n; j++)
                        new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];
                    for (int i = 0; i < m; i++)
                    {
                        if (i == mainRow)
                            continue;
                        for (int j = 0; j < n; j++)
                            new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                    }
                    table = new_table;
                }
                //заносим в result найденные значения X
                for (int i = 0; i < result.Length; i++)
                {
                    int k = basis.IndexOf(i + 1);
                    if (k != -1)
                        result[i] = table[k, 0];
                    else
                        result[i] = 0;
                }
                return table;
            }
            private bool IsItEnd()
            {
                bool flag = true;
                for (int j = 1; j < n; j++)
                {
                    if (table[m - 1, j] < 0)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }
            private int findMainCol()
            {
                int mainCol = 1;
                for (int j = 2; j < n; j++)
                    if (table[m - 1, j] < table[m - 1, mainCol])
                        mainCol = j;
                return mainCol;
            }
            private int findMainRow(int mainCol)
            {
                int mainRow = 0;
                for (int i = 0; i < m - 1; i++)
                    if (table[i, mainCol] > 0)
                    {
                        mainRow = i;
                        break;
                    }
                for (int i = mainRow + 1; i < m - 1; i++)
                    if ((table[i, mainCol] > 0) &&
                        ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                        mainRow = i;
                return mainRow;
            }
        }
        static void Main()
        {
            double[,] table =
                { 
                    {264, 12,  6},
                    {136,  4,  8}, 
                    {266,  3,  12},
                    {0,  -6, -4},
                };
            double[] result = new double[2];
            double[,] table_result;
            Simplex S = new Simplex(table);
            table_result = S.Calculate(result);
            Console.WriteLine("Решенная симплекс-таблица:");
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write(table_result[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Решение:");
            Console.WriteLine("X[1] = " + result[0]);
            Console.WriteLine("X[2] = " + result[1]);
            Console.ReadLine();
        }
    }
}

/*using System;

class SimplexMethod
{
    static void Main()
    {
        double[,] matrix = {
            {12, 4, 3, 1, 0, 0, 264},
            {3, 5, 14, 0, 1, 0, 236},
            {6, 4, 0, 0, 0, 1, 0},
            {-6, -4, 0, 0, 0, 0, 0}
        };

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        while (!IsOptimal(matrix))
        {
            int pivotCol = FindPivotColumn(matrix);
            int pivotRow = FindPivotRow(matrix, pivotCol);

            if (pivotRow == -1)
            {
                Console.WriteLine("Задача не имеет оптимального решения.");
                return;
            }

            Pivot(matrix, pivotRow, pivotCol);
        }

        double[] result = ExtractResult(matrix);
        PrintResult(result);
    }

    static bool IsOptimal(double[,] matrix)
    {
        int lastRow = matrix.GetLength(0) - 1;

        for (int i = 0; i < matrix.GetLength(1) - 1; i++)
        {
            if (matrix[lastRow, i] < 0)
            {
                return false;
            }
        }

        return true;
    }

    static int FindPivotColumn(double[,] matrix)
    {
        int lastRow = matrix.GetLength(0) - 1;
        int minIndex = 0;

        for (int i = 1; i < matrix.GetLength(1) - 1; i++)
        {
            if (matrix[lastRow, i] < matrix[lastRow, minIndex])
            {
                minIndex = i;
            }
        }

        return minIndex;
    }

    static int FindPivotRow(double[,] matrix, int pivotCol)
    {
        int pivotRow = -1;
        double minRatio = double.MaxValue;

        for (int i = 0; i < matrix.GetLength(0) - 1; i++)
        {
            if (matrix[i, pivotCol] > 0)
            {
                double ratio = matrix[i, matrix.GetLength(1) - 1] / matrix[i, pivotCol];

                if (ratio < minRatio)
                {
                    minRatio = ratio;
                    pivotRow = i;
                }
            }
        }

        return pivotRow;
    }

    static void Pivot(double[,] matrix, int pivotRow, int pivotCol)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        double pivotElement = matrix[pivotRow, pivotCol];

        for (int i = 0; i < cols; i++)
        {
            matrix[pivotRow, i] /= pivotElement;
        }

        for (int i = 0; i < rows; i++)
        {
            if (i != pivotRow)
            {
                double factor = matrix[i, pivotCol];
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] -= factor * matrix[pivotRow, j];
                }
            }
        }
    }

    static double[] ExtractResult(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        double[] result = new double[cols - 1];

        for (int i = 0; i < cols - 1; i++)
        {
            int count = 0;
            int index = -1;
            for (int j = 0; j < rows; j++)
            {
                if (matrix[j, i] == 1)
                {
                    count++;
                    index = j;
                }
            }

            if (count == 1)
            {
                result[i] = matrix[index, cols - 1];
            }
        }

        return result;
    }

    static void PrintResult(double[] result)
    {
        Console.WriteLine("Оптимальное решение:");
        for (int i = 0; i < result.Length; i++)
        {
            Console.WriteLine($"X{i + 1} = {result[i]}");
        }
    }
}*/