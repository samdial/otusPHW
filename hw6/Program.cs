using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] array100k = GenerateArray(100000);
        int[] array1M = GenerateArray(1000000);
        int[] array10M = GenerateArray(10000000);

        MeasureExecutionTime("Sequential", () => CalculateSumSequential(array100k));
        MeasureExecutionTime("Parallel with Threads", () => CalculateSumParallelWithThreads(array100k));
        MeasureExecutionTime("Parallel with LINQ", () => CalculateSumParallelWithLinq(array100k));

        MeasureExecutionTime("Sequential", () => CalculateSumSequential(array1M));
        MeasureExecutionTime("Parallel with Threads", () => CalculateSumParallelWithThreads(array1M));
        MeasureExecutionTime("Parallel with LINQ", () => CalculateSumParallelWithLinq(array1M));

        MeasureExecutionTime("Sequential", () => CalculateSumSequential(array10M));
        MeasureExecutionTime("Parallel with Threads", () => CalculateSumParallelWithThreads(array10M));
        MeasureExecutionTime("Parallel with LINQ", () => CalculateSumParallelWithLinq(array10M));
    }

    static int[] GenerateArray(int size)
    {
        var array = new int[size];
        var rand = new Random();
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(100);
        }
        return array;
    }

    static void MeasureExecutionTime(string label, Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        Console.WriteLine($"{label} execution time: {stopwatch.ElapsedMilliseconds} ms");
    }

    static int CalculateSumSequential(int[] array)
    {
        int sum = 0;
        foreach (var item in array)
        {
            sum += item;
        }
        return sum;
    }

    static int CalculateSumParallelWithThreads(int[] array)
    {
        const int NumThreads = 4;
        var sums = new List<int>();
        var threads = new Thread[NumThreads];

        for (int i = 0; i < NumThreads; i++)
        {
            int start = i * (array.Length / NumThreads);
            int end = (i == NumThreads - 1) ? array.Length : (i + 1) * (array.Length / NumThreads);
            threads[i] = new Thread(() =>
            {
                int localSum = 0;
                for (int j = start; j < end; j++)
                {
                    localSum += array[j];
                }
                lock (sums)
                {
                    sums.Add(localSum);
                }
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return sums.Sum();
    }

    static int CalculateSumParallelWithLinq(int[] array)
    {
        return array.AsParallel().Sum();
    }
}
