using System;
using System.IO;

namespace ExceptionsDemo;

public static class ExceptionDemoApplication
{
    public static void Run(string fileName, TextWriter output)
    {
        ArgumentNullException.ThrowIfNull(output);

        output.WriteLine("=== Start av programmet ===");

        try
        {
            output.WriteLine("Försöker läsa fil och räkna...");
            double result = NumberFileProcessor.ProcessFile(fileName);
            output.WriteLine($"\nResultat: {result}");
        }
        catch (FileNotFoundException ex)
        {
            output.WriteLine($"Filen hittades inte: {ex.Message}");
        }
        catch (FormatException ex)
        {
            output.WriteLine($"Formatfel: {ex.Message}");
        }
        catch (Exception ex)
        {
            output.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            output.WriteLine("Cleanup: Logging avslutat anrop.");
        }

        output.WriteLine("Programmet avslutas normalt.");
    }
}
