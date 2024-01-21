using System;

class SimplexMethod
{
    static void Main()
    {
        double[,] matrix = {
            {4, 2, 6, 1, 0, 0, 166},
            {10, 10, 12, 0, 1, 0, 138},
            {6, 20, 0, 0, 0, 1, 0},
            {-6, -20, 0, 0, 0, 0, 0}
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
}
