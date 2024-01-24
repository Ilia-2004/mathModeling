using System;

class SearchProgram
{
    static void Main()
    {
        // Пример неупорядоченного массива и упорядоченного массива
        int[] unsortedArray = { 10, 5, 3, 8, 7, 1, 2, 6, 9, 4 };
        int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Неупорядоченный массив:");
        PrintArray(unsortedArray);

        Console.WriteLine("Упорядоченный массив:");
        PrintArray(sortedArray);

        // Линейный поиск с барьером в неупорядоченном массиве
        int linearSearchResult = LinearSearchWithSentinel(unsortedArray, 7);
        Console.WriteLine($"Линейный поиск с барьером в неупорядоченном массиве: Индекс = {linearSearchResult}");

        // Сортировка шелла
        ShellSort(unsortedArray);
        Console.WriteLine("Отсортированный неупорядоченный массив:");
        PrintArray(unsortedArray);

        // Интерполяционный поиск в упорядоченном массиве
        int interpolationSearchResult = InterpolationSearch(sortedArray, 7);
        Console.WriteLine($"Интерполяционный поиск в упорядоченном массиве: Индекс = {interpolationSearchResult}");
    }

    // Линейный поиск с барьером в неупорядоченном массиве
    static int LinearSearchWithSentinel(int[] array, int target)
    {
        int n = array.Length;
        int lastElement = array[n - 1];

        array[n - 1] = target; // Устанавливаем барьер

        int i = 0;
        while (array[i] != target)
        {
            i++;
        }

        array[n - 1] = lastElement; // Восстанавливаем последний элемент массива

        if (i < n - 1 || array[n - 1] == target)
        {
            return i;
        }
        else
        {
            return -1; // Элемент не найден
        }
    }

    // Сортировка шелла
    static void ShellSort(int[] array)
    {
        int n = array.Length;
        int gap = n / 2;

        while (gap > 0)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j = i;

                while (j >= gap && array[j - gap] > temp)
                {
                    array[j] = array[j - gap];
                    j -= gap;
                }

                array[j] = temp;
            }

            gap /= 2;
        }
    }

    // Интерполяционный поиск в упорядоченном массиве
    static int InterpolationSearch(int[] array, int target)
    {
        int low = 0;
        int high = array.Length - 1;

        while (low <= high && target >= array[low] && target <= array[high])
        {
            int pos = low + ((target - array[low]) * (high - low) / (array[high] - array[low]));

            if (array[pos] == target)
            {
                return pos;
            }
            else if (array[pos] < target)
            {
                low = pos + 1;
            }
            else
            {
                high = pos - 1;
            }
        }

        return -1; // Элемент не найден
    }

    // Вспомогательный метод для вывода массива на консоль
    static void PrintArray(int[] array)
    {
        foreach (var element in array)
        {
            Console.Write(element + " ");
        }
        Console.WriteLine();
    }
}
