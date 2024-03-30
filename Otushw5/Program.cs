using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelFileReading
{
    internal class Program
    {
        static async Task Main()
        {
            const string directoryPath = ".";
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }
            Console.WriteLine("Counting white spaces in all files");
            Console.WriteLine($"\tin directory: {Path.GetFullPath(directoryPath)}\n");
            Dictionary<string, int> dict = await CalculateSpacesInDirectoryAsync(directoryPath);
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} has {kvp.Value} spaces");
            }
        }

        static async Task<Dictionary<string, int>> CalculateSpacesInDirectoryAsync(string directoryPath)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string[] filePaths = Directory.GetFiles(directoryPath);
            Task<int>[] tasks = new Task<int>[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                tasks[i] = CalculateSpacesInFileAsync(filePaths[i]);
            }
            await Task.WhenAll(tasks);

            Dictionary<string, int> resultDictionary = new();
            for (int i = 0; i < filePaths.Length; i++)
            {
                resultDictionary[filePaths[i]] = tasks[i].Result;
            }
            stopwatch.Stop();
            Console.WriteLine($"The operation took {stopwatch.Elapsed}\n");
            return resultDictionary;
        }

        static async Task<int> CalculateSpacesInFileAsync(string filePath)
        {
            string text = await File.ReadAllTextAsync(filePath);
            return text.Count(char.IsWhiteSpace);
        }
    }
}
