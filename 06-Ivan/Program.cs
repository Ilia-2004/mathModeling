using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

class TransportProblem
{
    static void Main()
    {
        int[] supply = { 380, 450, 420 };
        int[] demand = { 230, 200, 400, 270, 230 };
        int[,] cost =
        {
            { 15, 9, 7, 13, 10 },
            { 14, 10, 3, 14, 7 },
            { 16, 8, 10, 12, 17 }
        };
        // int[] supply = { 180, 210, 190 };
        // int[] demand = { 100, 150, 130, 120, 80 };
        // int[,] cost =
        // {
        //     { 6, 4, 5, 7, 2 },
        //     { 5, 3, 7, 4, 8 },
        //     { 4, 7, 3, 5, 6 }
        // };
        int[,] result = SolveTransportProblem(supply, demand, cost);
        int totalCostBeforeOptimization = CalculateTotalCost(result, cost);
        Console.WriteLine("Распределение до оптимизации:");
        PrintResult(result);
        Console.WriteLine("Общая стоимость до оптимизации: " + totalCostBeforeOptimization);
        int totalCostAfterOptimization = OptimizeUsingPotentialMethod(result, cost, supply, demand);
        int[,] result2 = SolveTransportProblemUsingMinCostMethod(supply, demand, cost);
        int totalCost2 = CalculateTotalCost(result2, cost);
        Console.WriteLine("Распределение после оптимизации:");
        PrintResult(result2);
        Console.WriteLine("Общая стоимость после оптимизации: " + totalCost2);
    }
    static int OptimizeUsingPotentialMethod(int[,] distribution, int[,] cost, int[] supply, int[] demand)
    {
        int m = supply.Length;
        int n = demand.Length;
        int[] u = new int[m];
        int[] v = new int[n];
        bool[] usedU = new bool[m];
        bool[] usedV = new bool[n];
        // Нахождение начальных потенциалов u и v
       // FindInitialPotentials(distribution, cost, u, v, usedU, usedV);
       // Оптимизация распределения
        bool optimized;
        do
        {
            optimized = true;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (distribution[i, j] == 0) // Работаем только с пустыми клетками
                    {
                        int delta = cost[i, j] - (u[i] + v[j]);
                        if (delta < 0)
                        {
                            optimized = false;
                            Redistribute(distribution, cost, u, v, i, j);
                            //FindInitialPotentials(distribution, cost, u, v, usedU, usedV); // Пересчитываем потенциалы
                        }
                    }
                }
            }
        } while (!optimized);
        return CalculateTotalCost(distribution, cost); 
    }
    static int[,] SolveTransportProblemUsingMinCostMethod(int[] supply, int[] demand, int[,] cost)
    {
        int m = supply.Length;
        int n = demand.Length;
        int[,] distribution = new int[m, n];
        // Копии массивов предложения и спроса
        int[] supplyLeft = (int[])supply.Clone();
        int[] demandLeft = (int[])demand.Clone();
        while (supplyLeft.Sum() > 0 && demandLeft.Sum() > 0)
        {
            // Находим клетку с минимальной стоимостью
            int minCost = int.MaxValue;
            int minI = -1, minJ = -1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (supplyLeft[i] > 0 && demandLeft[j] > 0 && cost[i, j] < minCost)
                    {
                        minCost = cost[i, j];
                        minI = i;
                        minJ = j;
                    }
                }
            }
            // Распределяем груз в найденной клетке
            int allocation = Math.Min(supplyLeft[minI], demandLeft[minJ]);
            distribution[minI, minJ] = allocation;
            supplyLeft[minI] -= allocation;
            demandLeft[minJ] -= allocation;
        }
        return distribution;
    }
    static void Redistribute(int[,] distribution, int[,] cost, int[] u, int[] v, int startI, int startJ)
    {
        var path = FindCycle(distribution, startI, startJ);
        if (path == null || path.Count <= 1) return; // Если цикл не найден, возвращаем false
        // Поиск минимального количества груза для перераспределения
        int minTransfer = int.MaxValue;
        for (int k = 1; k < path.Count; k += 2)
        {
            int i = path[k].Item1;
            int j = path[k].Item2;
            minTransfer = Math.Min(minTransfer, distribution[i, j]);
        }
        // Перераспределение груза
        for (int k = 0; k < path.Count; k++)
        {
            int i = path[k].Item1;
            int j = path[k].Item2;
            if (k % 2 == 0)
                distribution[i, j] += minTransfer; 
            else
                distribution[i, j] -= minTransfer; 
        }
    }
    static List<Tuple<int, int>> FindCycle(int[,] distribution, int startI, int startJ)
    {
        int m = distribution.GetLength(0);
        int n = distribution.GetLength(1);
        bool[,] visited = new bool[m, n];
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        if (DFS(distribution, visited, path, startI, startJ, startI, startJ, true))
        {
            return path;
        }
        return new List<Tuple<int, int>>(); // Возвращаем пустой список, если цикл не найден
    }
    static bool DFS(int[,] distribution, bool[,] visited, List<Tuple<int, int>> path, int i, int j, int startI, int startJ, bool isHorizontal)
    {
        if (visited[i, j]) return false;
        path.Add(new Tuple<int, int>(i, j));
        visited[i, j] = true;
        if (i == startI && j == startJ && path.Count > 1) return true;
        int m = distribution.GetLength(0);
        int n = distribution.GetLength(1);
        if (isHorizontal)
        {
            for (int newJ = 0; newJ < n; newJ++)
            {
                if (newJ != j && distribution[i, newJ] > 0)
                {
                    if (DFS(distribution, visited, path, i, newJ, startI, startJ, !isHorizontal))
                        return true;
                }
            }
        }
        else
        {
            for (int newI = 0; newI < m; newI++)
            {
                if (newI != i && distribution[newI, j] > 0)
                {
                    if (DFS(distribution, visited, path, newI, j, startI, startJ, !isHorizontal))
                        return true;
                }
            }
        }
        // Если цикл не найден, откатываем последний шаг
        path.RemoveAt(path.Count - 1);
        visited[i, j] = false;
        return false;
    }
    static int[,] SolveTransportProblem(int[] supply, int[] demand, int[,] cost)
    {
        int[,] result = new int[supply.Length, demand.Length];
        int[] supplyLeft = (int[])supply.Clone();
        int[] demandLeft = (int[])demand.Clone();
        Console.WriteLine("Начальное распределение:");
        PrintResult(result);
        int i = 0, j = 0;
        while (i < supply.Length && j < demand.Length)
        {
            int allocation = Math.Min(supplyLeft[i], demandLeft[j]);
            result[i, j] = allocation;
            supplyLeft[i] -= allocation;
            demandLeft[j] -= allocation;
            Console.WriteLine($"Распределяем {allocation} единиц из А{i+1} в В{j+1}");
            PrintResult(result);
            if (supplyLeft[i] == 0) i++;
            else if (demandLeft[j] == 0) j++;
        }
        return result;
    }
    static int CalculateTotalCost(int[,] result, int[,] cost)
    {
        int totalCost = 0;
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                totalCost += result[i, j] * cost[i, j];
            }
        }
        return totalCost;
    }
    static void PrintResult(int[,] result)
    {
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                Console.Write(result[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}