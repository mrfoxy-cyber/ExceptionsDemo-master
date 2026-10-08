namespace ExceptionDemo.Tests;

internal sealed class TemporaryTestFile : IDisposable
{
    private TemporaryTestFile(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static TemporaryTestFile Create(string content = "")
    {
        string path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            $"ExceptionsDemo-{Guid.NewGuid():N}.txt");
        File.WriteAllText(path, content);
        return new TemporaryTestFile(path);
    }

    public void Dispose()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}
