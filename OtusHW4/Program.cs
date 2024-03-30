using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = @"D:\leetcode";

        FileSearcher searcher = new FileSearcher();
        searcher.FileFound += (sender, e) =>
        {
            string filePath = Path.Combine(directoryPath, e.FileName);
            Console.WriteLine($"File found: {e.FileName}");
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
            }
            else
            {
                Console.WriteLine($"File '{e.FileName}' not found.");
            }
        };
        searcher.SearchCancelled += (sender, e) => Console.WriteLine("Search cancelled.");

        searcher.SearchFiles(directoryPath);

        var files = Directory.GetFiles(directoryPath);
        var maxFileSize = files.GetMax(file =>
        {
            var fileInfo = new FileInfo(file);
            return fileInfo.Length;
        });

        Console.WriteLine($"Max file size: {maxFileSize}");
    }
}
