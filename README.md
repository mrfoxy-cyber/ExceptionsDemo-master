# Exceptions Demo

## What this assignment is about

This assignment practices exception handling and automated testing in C#.

The application reads the first line of `numbers.txt`, converts it to an integer, and calculates:

```text
100 / number
```

The main goal is not the calculation itself. The goal is to learn how a program behaves when something goes wrong and how to:

- use `try`, `catch`, and `finally`;
- throw an exception deliberately with `throw`;
- catch specific exception types before a general `Exception`;
- preserve useful error information;
- release file resources after success or failure;
- write automated xUnit tests for successful and failing scenarios;
- structure tests using Given, When, and Then;
- connect tests to requirements and acceptance criteria.

## Program flow

1. `Program` creates the path to `numbers.txt`.
2. `ExceptionDemoApplication` controls console output and handles errors.
3. `NumberFileProcessor` opens the file and reads its first line.
4. The text is converted to an `int`.
5. Zero is rejected.
6. For valid input, the method returns `100.0 / number`.

## Exceptions covered

### `ArgumentException`

Occurs when the supplied filename is null, empty, or whitespace.

Example:

```csharp
NumberFileProcessor.ProcessFile(" ");
```

### `FileNotFoundException`

Occurs when the directory exists but the requested file does not.

### `DirectoryNotFoundException`

Occurs when part of the directory path does not exist.

This differs from `FileNotFoundException`: one represents a missing file, while the other represents a missing directory.

### `UnauthorizedAccessException`

Occurs when the program is not allowed to open the supplied path. The test uses a directory path where a readable file path is expected.

### `IOException`

Represents a general input/output failure. The test creates this condition by holding an exclusive lock on the file before attempting to process it.

### `InvalidOperationException`

Used when the file exists but is empty. The operation cannot continue because there is no number to process.

### `FormatException`

Occurs when the first line cannot be converted to an integer.

Examples include:

```text
hello
9+10
12.5
```

### `OverflowException`

Occurs when the text represents a number outside the range supported by `Int32`.

Example:

```text
999999999999999999999
```

### `DivideByZeroException`

The application throws this exception deliberately when the number is zero:

```csharp
if (number == 0)
{
    throw new DivideByZeroException("Kan inte dividera med noll.");
}
```

The calculation uses `double`, which would normally produce infinity when divided by zero. The explicit check enforces the assignment's business rule instead.

### `ArgumentNullException`

Occurs when `ExceptionDemoApplication.Run` receives a null output writer.

### `ObjectDisposedException`

Occurs when the application attempts to write through an output writer that has already been disposed.

### `PathTooLongException`

Occurs when the supplied file path is longer than the platform supports.

## Exception handling principles

Specific exceptions should be handled before the general fallback:

```csharp
catch (FileNotFoundException ex)
{
    // Handle a missing file.
}
catch (FormatException ex)
{
    // Handle invalid number text.
}
catch (Exception ex)
{
    // Handle unexpected exceptions.
}
```

`finally` runs whether the operation succeeds or throws an exception. It is useful for cleanup and logging. File resources in `NumberFileProcessor` are managed with `using`, which also guarantees disposal when an exception occurs.

## Tests and traceability

The tests use xUnit. Each acceptance test contains explicit Given, When, and Then sections:

```csharp
// Given
using TemporaryTestFile file = TemporaryTestFile.Create("4");

// When
double result = NumberFileProcessor.ProcessFile(file.Path);

// Then
Assert.Equal(25.0, result);
```

Each test is connected to the specification with traits:

```csharp
[Trait(Traceability.Requirement, "FR-04")]
[Trait(Traceability.UseCase, "UC-001")]
[Trait(Traceability.AcceptanceCriterion, "AC-01")]
```

The traceability tests ensure that:

- every documented requirement has a linked test;
- every acceptance criterion has a linked test;
- tests do not reference unknown requirement or acceptance-criterion IDs;
- every acceptance test has all three traceability traits.

The detailed specifications are available in:

- `Requirements.md`
- `UseCase-ProcessNumberFile.md`
- `TESTING.md`

## Running the application

From `P:\LexiconGitForks\ExceptionsDemo-master`:

```powershell
dotnet run
```

Change the first line of `numbers.txt` to practice different outcomes.

## Running the tests

From `P:\LexiconGitForks`:

```powershell
dotnet test .\ExceptionsDemo-master\ExceptionsDemo.slnx
```

Run one requirement:

```powershell
dotnet test .\ExceptionsDemo-master\ExceptionsDemo.slnx --filter "Requirement=FR-09"
```

Run one acceptance criterion:

```powershell
dotnet test .\ExceptionsDemo-master\ExceptionsDemo.slnx --filter "AcceptanceCriterion=AC-07"
```

Run every test for the use case:

```powershell
dotnet test .\ExceptionsDemo-master\ExceptionsDemo.slnx --filter "UseCase=UC-001"
```

## Expected result

The complete suite contains:

- 15 acceptance tests;
- 2 specification-traceability tests;
- 17 tests in total.

All tests should pass before the assignment is considered complete.
