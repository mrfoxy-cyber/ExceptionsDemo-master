using System;
using System.IO;

namespace ExceptionsDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
        ExceptionDemoApplication.Run(path, Console.Out);
    }
}
