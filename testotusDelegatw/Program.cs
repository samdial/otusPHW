using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DelegatesEvents
{
    internal class Program
    {
        static void Main()
        {
            TestGetMax();
            TestFileFinderEvents(@"C:\Windows");
        }

        private static void TestGetMax()
        {
            List<int> numbers = new List<int> { 3, 7, 2, 8, 4 };
            Console.WriteLine("The maximum number in the collection is:");
            Console.WriteLine(numbers.GetMax(x => x));
            Console.WriteLine();
        }

        private static void TestFileFinderEvents(string folderPath)
        {
            FileFinder finder = new FileFinder();
            finder.FileFound += HandleFileFound;
            finder.StartSearch(folderPath);
        }

        private static void HandleFileFound(object? sender, FileEventArgs e)
        {
            Console.WriteLine($"File found: {e.FileName}");
        }
    }



    internal class FileEventArgs : EventArgs
    {
        public string FileName { get; }

        public FileEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }

}
