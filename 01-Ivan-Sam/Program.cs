using System;

public class Cargo
{
    public int MaxAmount { get; set; }
    public int Profit { get; set; }
    public int Weight { get; set; }
}

public class ShipLoadingProblem
{
    public int[] CalculateMaxProfitLoad(Cargo[] cargos, int maxCapacity)
    {
        int n = cargos.Length;
        int[,] dp = new int[n + 1, maxCapacity + 1];
        int[,] keep = new int[n + 1, maxCapacity + 1];

        // Initialize the dp array.
        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= maxCapacity; w++)
            {
                if (i == 0 || w == 0)
                    dp[i, w] = 0;
                else if (cargos[i - 1].Weight <= w)
                {
                    int maxValWithCurr = cargos[i - 1].Profit +
                        dp[i - 1, w - cargos[i - 1].Weight];
                    if (maxValWithCurr > dp[i - 1, w])
                    {
                        dp[i, w] = maxValWithCurr;
                        keep[i, w] = 1;
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        // Find out which cargos to keep
        int[] quantities = new int[n];
        int remainingCapacity = maxCapacity;
        for (int i = n; i > 0 && remainingCapacity > 0; i--)
        {
            if (keep[i, remainingCapacity] == 1)
            {
                quantities[i - 1] = Math.Min(cargos[i - 1].MaxAmount,
                    remainingCapacity / cargos[i - 1].Weight);
                remainingCapacity -= cargos[i - 1].Weight * quantities[i - 1];
            }
        }

        return quantities;
    }
}

public class Program
{
    public static void Main()
    {
        Cargo[] cargos = new Cargo[]
        {
            new Cargo { MaxAmount = 5, Profit = 68, Weight = 16 },
            new Cargo { MaxAmount = 5, Profit = 50, Weight = 15 },
            new Cargo { MaxAmount = 5, Profit = 41, Weight = 15 },
            new Cargo { MaxAmount = 4, Profit = 51, Weight = 20 },
            new Cargo { MaxAmount = 4, Profit = 66, Weight = 11 }
        };
        int maxCapacity = 108;
        // Cargo[] cargos = new Cargo[]
        // {
        //     new Cargo { MaxAmount = 5, Profit = 44, Weight = 11 },
        //     new Cargo { MaxAmount = 6, Profit = 44, Weight = 16 },
        //     new Cargo { MaxAmount = 6, Profit = 57, Weight = 18 },
        //     new Cargo { MaxAmount = 4, Profit = 64, Weight = 17 },
        //     new Cargo { MaxAmount = 5, Profit = 53, Weight = 16 }
        // };
        // int maxCapacity = 159;

        ShipLoadingProblem problem = new ShipLoadingProblem();
        int[] maxProfitLoad = problem.CalculateMaxProfitLoad(cargos, maxCapacity);

        int totalProfit = 0;
        int totalWeight = 0;

        Console.WriteLine("Загрузка для максимальной прибыли:");
        for (int i = 0; i < maxProfitLoad.Length; i++)
        {
            int profitByCargo = maxProfitLoad[i] * cargos[i].Profit;
            int weightByCargo = maxProfitLoad[i] * cargos[i].Weight;
            totalProfit += profitByCargo;
            totalWeight += weightByCargo;

            Console.WriteLine($"Груз {i+1}:");
            Console.WriteLine($"\tКоличество: {maxProfitLoad[i]} ед.");
            Console.WriteLine($"\tПрибыль с единицы: {cargos[i].Profit}");
            Console.WriteLine($"\tВес единицы: {cargos[i].Weight}");
            Console.WriteLine($"\tОбщая прибыль от груза {i+1}: {profitByCargo}");
            Console.WriteLine($"\tОбщий вес груза {i+1}: {weightByCargo}\n");
        }

        Console.WriteLine($"Общая прибыль: {totalProfit}");
        Console.WriteLine($"Общий вес: {totalWeight}");
        Console.WriteLine($"Остаточная грузоподъемность: {maxCapacity - totalWeight}");
    }
}
