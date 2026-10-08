using ExceptionsDemo;

namespace ExceptionDemo.Tests;

public sealed class NumberFileProcessorTests
{
    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-01")]
    [Trait(Traceability.Requirement, "FR-01")]
    [Trait(Traceability.Requirement, "FR-02")]
    [Trait(Traceability.Requirement, "FR-03")]
    [Trait(Traceability.Requirement, "FR-04")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenFileContainsFour_ReturnsTwentyFive()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("4");

        // When
        double result = NumberFileProcessor.ProcessFile(file.Path);

        // Then
        Assert.Equal(25.0, result);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-02")]
    [Trait(Traceability.Requirement, "FR-04")]
    public void ProcessFile_WhenResultIsFractional_PreservesDecimalValue()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("8");

        // When
        double result = NumberFileProcessor.ProcessFile(file.Path);

        // Then
        Assert.Equal(12.5, result);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-03")]
    [Trait(Traceability.Requirement, "FR-05")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Given
        string path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            $"ExceptionsDemo-missing-{Guid.NewGuid():N}.txt");

        // When
        Action action = () => NumberFileProcessor.ProcessFile(path);

        // Then
        Assert.Throws<FileNotFoundException>(action);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-04")]
    [Trait(Traceability.Requirement, "FR-06")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenFileIsEmpty_ThrowsInvalidOperationException()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create();

        // When
        Action action = () => NumberFileProcessor.ProcessFile(file.Path);

        // Then
        InvalidOperationException exception =
            Assert.Throws<InvalidOperationException>(action);
        Assert.Contains("tom", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-05")]
    [Trait(Traceability.Requirement, "FR-07")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenContentIsNotNumeric_ThrowsFormatException()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("hello");

        // When
        Action action = () => NumberFileProcessor.ProcessFile(file.Path);

        // Then
        Assert.Throws<FormatException>(action);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-06")]
    [Trait(Traceability.Requirement, "FR-08")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenNumberIsTooLarge_ThrowsOverflowException()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create(
            "999999999999999999999");

        // When
        Action action = () => NumberFileProcessor.ProcessFile(file.Path);

        // Then
        Assert.Throws<OverflowException>(action);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-07")]
    [Trait(Traceability.Requirement, "FR-09")]
    [Trait(Traceability.Requirement, "FR-11")]
    public void ProcessFile_WhenNumberIsZero_ThrowsDivideByZeroException()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("0");

        // When
        Action action = () => NumberFileProcessor.ProcessFile(file.Path);

        // Then
        DivideByZeroException exception =
            Assert.Throws<DivideByZeroException>(action);
        Assert.Contains("noll", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-08")]
    [Trait(Traceability.Requirement, "FR-10")]
    public void ProcessFile_WhenFileNameIsBlank_ThrowsArgumentException()
    {
        // Given
        const string fileName = " ";

        // When
        Action action = () => NumberFileProcessor.ProcessFile(fileName);

        // Then
        ArgumentException exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("fileName", exception.ParamName);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-10")]
    [Trait(Traceability.Requirement, "FR-13")]
    public void ProcessFile_WhenDirectoryDoesNotExist_ThrowsDirectoryNotFoundException()
    {
        // Given
        string path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            $"ExceptionsDemo-missing-directory-{Guid.NewGuid():N}",
            "numbers.txt");

        // When
        Action action = () => NumberFileProcessor.ProcessFile(path);

        // Then
        Assert.Throws<DirectoryNotFoundException>(action);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-11")]
    [Trait(Traceability.Requirement, "FR-14")]
    public void ProcessFile_WhenPathIsDirectory_ThrowsUnauthorizedAccessException()
    {
        // Given
        string directoryPath = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            $"ExceptionsDemo-directory-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directoryPath);

        try
        {
            // When
            Action action = () => NumberFileProcessor.ProcessFile(directoryPath);

            // Then
            Assert.Throws<UnauthorizedAccessException>(action);
        }
        finally
        {
            Directory.Delete(directoryPath);
        }
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-12")]
    [Trait(Traceability.Requirement, "FR-15")]
    public void ProcessFile_WhenFileIsExclusivelyLocked_ThrowsIOException()
    {
        // Given
        using TemporaryTestFile file = TemporaryTestFile.Create("4");
        using var lockStream = new FileStream(
            file.Path,
            FileMode.Open,
            FileAccess.ReadWrite,
            FileShare.None);

        // When
        Action action = () => NumberFileProcessor.ProcessFile(file.Path);

        // Then
        Assert.Throws<IOException>(action);
    }

    [Fact]
    [Trait(Traceability.UseCase, "UC-001")]
    [Trait(Traceability.AcceptanceCriterion, "AC-15")]
    [Trait(Traceability.Requirement, "FR-18")]
    public void ProcessFile_WhenPathIsTooLong_ThrowsPathTooLongException()
    {
        // Given
        string path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            new string('a', 40_000));

        // When
        Action action = () => NumberFileProcessor.ProcessFile(path);

        // Then
        Assert.Throws<PathTooLongException>(action);
    }
}
