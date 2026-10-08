using System;
using System.IO;

namespace ExceptionsDemo;

public static class NumberFileProcessor
{
    public static double ProcessFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(
                "Filnamn får inte vara tomt eller null.",
                nameof(fileName));
        }

        using var reader = new StreamReader(fileName);
        string? line = reader.ReadLine();

        if (line is null)
        {
            throw new InvalidOperationException("Filen är tom.");
        }

        int number = int.Parse(line);

        if (number == 0)
        {
            throw new DivideByZeroException("Kan inte dividera med noll.");
        }

        return 100.0 / number;
    }
}
