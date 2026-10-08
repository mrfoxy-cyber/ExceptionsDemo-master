using ExceptionsDemo;

namespace ExceptionDemo.Tests;

public sealed class ExceptionDemoApplicationTests
{
    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-09")]
    [Trait(Traceability.Requirement, "FR-12")]
    public void Run_WhenProcessingFails_ReportsErrorAndFinishesNormally()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("0");
        using var output = new StringWriter();

        // When
        ExceptionDemoApplication.Run(file.Path, output);

        // Then
        string text = output.ToString();
        Assert.Contains(nameof(DivideByZeroException), text);
        Assert.Contains("Programmet avslutas normalt.", text);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-13")]
    [Trait(Traceability.Requirement, "FR-16")]
    public void Run_WhenOutputIsNull_ThrowsArgumentNullException()
    {
        // Given
        TextWriter output = null!;

        // When
        Action action = () => ExceptionDemoApplication.Run("numbers.txt", output);

        // Then
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal("output", exception.ParamName);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-14")]
    [Trait(Traceability.Requirement, "FR-17")]
    public void Run_WhenOutputIsDisposed_ThrowsObjectDisposedException()
    {
        // Given
        var output = new StreamWriter(new MemoryStream());
        output.Dispose();

        // When
        Action action = () => ExceptionDemoApplication.Run("numbers.txt", output);

        // Then
        Assert.Throws<ObjectDisposedException>(action);
    }
}
