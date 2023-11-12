using System;
using System.Collections.Generic;

namespace secondTask
{
    internal abstract class Program
    {
        private static int LinearSearch(IList<int> arr, int value)
        {
            var last = arr[arr.Count - 1];
            arr[arr.Count - 1] = value;
            var i = 0;
            
            while (arr[i] != value)
                i++;
            
            arr[arr.Count - 1] = last;
            if ((i < arr.Count - 1) || (value == last))
                return i;
            else
                return -1;
        }

        private static void QuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            var pivot = Partition(arr, left, right);
            QuickSort(arr, left, pivot - 1);
            QuickSort(arr, pivot + 1, right);
        }

        private static int Partition(int[] arr, int left, int right)
        {
            var pivot = arr[right];
            var i = left - 1;
            for (var j = left; j < right; j++)
            {
                if (arr[j] >= pivot) continue;
                i++;
                Swap(ref arr[i], ref arr[j]);
            }
            Swap(ref arr[i + 1], ref arr[right]);
            return i + 1;
        }

        private static void Swap(ref int a, ref int b) => (a, b) = (b, a);

        private static int InterpolationSearch(IReadOnlyList<int> arr, int value)
        {
            var low = 0;
            var high = arr.Count - 1;
            
            while ((low <= high) && (value >= arr[low]) && (value <= arr[high]))
            {
                var pos = low + (((high - low) / (arr[high] - arr[low])) * (value - arr[low]));
                if (arr[pos] == value)
                    return pos;
                if (arr[pos] < value)
                    low = pos + 1;
                else
                    high = pos - 1;
            }
            return -1;
        }

        private static void Main(string[] args)
        {
            int[] unsortedArray = { 5, 3, 7, 2, 9, 1, 4, 6, 8 };
            int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            const int searchValue = 7;

            Console.WriteLine("Unsorted array: [{0}]", string.Join(", ", unsortedArray));
            Console.WriteLine("Sorted array: [{0}]", string.Join(", ", sortedArray));
            Console.WriteLine("Search value: {0}", searchValue);

            Console.WriteLine("\nLinear search with barrier:");
            var index = LinearSearch(unsortedArray, searchValue);
            if (index == -1)
                Console.WriteLine("Value {0} not found in the array.", searchValue);
            else
                Console.WriteLine("Value {0} found at index {1}.", searchValue, index);

            Console.WriteLine("\nQuick sort:");
            QuickSort(unsortedArray, 0, unsortedArray.Length - 1);
            Console.WriteLine("Sorted array: [{0}]", string.Join(", ", unsortedArray));

            Console.WriteLine("\nInterpolation search:");
            index = InterpolationSearch(sortedArray, searchValue);
            if (index == -1)
                Console.WriteLine("Value {0} not found in the array.", searchValue);
            else
                Console.WriteLine("Value {0} found at index {1}.", searchValue, index);
        }
    }
}
