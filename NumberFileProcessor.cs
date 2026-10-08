using System;
using System.IO;

namespace ExceptionsDemo;

public static class NumberFileProcessor
{
    private const double Dividend = 100.0;

    public static double ProcessFile(string fileName)
    {
        ValidateFileName(fileName);

        string firstLine = ReadFirstLine(fileName);
        int divisor = ParseWholeNumber(firstLine);

        EnsureDivisorIsNotZero(divisor);

        return CalculateResult(divisor);
    }

    private static void ValidateFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(
                "Filnamn får inte vara tomt eller null.",
                nameof(fileName));
        }
    }

    private static string ReadFirstLine(string fileName)
    {
        // StreamReader can automatically throw FileNotFoundException,
        // DirectoryNotFoundException, UnauthorizedAccessException,
        // IOException, or PathTooLongException when it opens the file.
        using var reader = new StreamReader(fileName);

        // ReadLine can automatically throw IOException if reading fails.
        string? firstLine = reader.ReadLine();

        if (firstLine is null)
        {
            throw new InvalidOperationException("Filen är tom.");
        }

        return firstLine;
    }

    private static int ParseWholeNumber(string text)
    {
        // int.Parse automatically throws FormatException when the text is
        // not a whole number, or OverflowException when it is outside
        // the range supported by Int32.
        return int.Parse(text);
    }

    private static void EnsureDivisorIsNotZero(int divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Kan inte dividera med noll.");
        }
    }

    private static double CalculateResult(int divisor)
    {
        return Dividend / divisor;
    }
}
