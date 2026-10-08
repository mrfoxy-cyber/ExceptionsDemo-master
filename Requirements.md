# Requirements: Process a Number File

## Purpose

The application shall read a whole number from a text file, divide 100 by that number, and clearly report expected errors without terminating unexpectedly.

## Functional requirements

| ID | Requirement | Priority |
|---|---|---|
| FR-01 | The application shall read `numbers.txt` from its execution directory. | Must |
| FR-02 | The application shall use the first line of the file as input. | Must |
| FR-03 | The input shall be parsed as a 32-bit whole number. | Must |
| FR-04 | For valid non-zero input, the application shall calculate `100.0 / number`. | Must |
| FR-05 | A missing file shall be reported as a file-not-found error. | Must |
| FR-06 | An empty file shall be reported as an invalid-operation error. | Must |
| FR-07 | Non-numeric input shall be reported as a format error. | Must |
| FR-08 | A number outside the 32-bit integer range shall be reported as an overflow error. | Must |
| FR-09 | Zero shall be rejected with a divide-by-zero error before the calculation is performed. | Must |
| FR-10 | A blank or null file name shall be rejected with an argument error. | Must |
| FR-11 | The file reader shall be closed whether processing succeeds or fails. | Must |
| FR-12 | The console application shall finish normally after displaying either a result or an error message. | Should |
| FR-13 | A file path inside a missing directory shall be reported as a directory-not-found error. | Must |
| FR-14 | A path that cannot be read because access is denied shall be reported as an unauthorized-access error. | Must |
| FR-15 | A file that cannot be opened because it is exclusively locked shall be reported as an input/output error. | Must |
| FR-16 | A null output writer shall be rejected with an argument-null error. | Must |
| FR-17 | A disposed output writer shall be reported as an object-disposed error. | Must |
| FR-18 | A path exceeding the supported length shall be reported as a path-too-long error. | Must |

## Business rules

| ID | Rule |
|---|---|
| BR-01 | Only the first line of the file is processed. |
| BR-02 | The first line must contain one valid `int` value. |
| BR-03 | Zero is not a valid divisor. |
| BR-04 | The calculation uses floating-point division so decimal results are preserved. |
| BR-05 | Specific, expected exceptions take precedence over a general fallback exception. |

## Non-functional requirements

- **Clarity:** Error output shall identify the exception type or provide a specific user-facing explanation.
- **Reliability:** Resources shall be released after both successful and failed processing.
- **Testability:** File-processing behavior shall be callable independently of console input and output.
- **Maintainability:** Each requirement and acceptance criterion shall be traceable to at least one automated test.

## Acceptance summary

| Requirement | Input or condition | Expected result |
|---|---|---|
| FR-04 | File contains `4` | Result is `25` |
| FR-05 | File does not exist | `FileNotFoundException` is reported |
| FR-06 | File is empty | `InvalidOperationException` is reported |
| FR-07 | File contains `hello` | `FormatException` is reported |
| FR-08 | File contains `999999999999999999999` | `OverflowException` is reported |
| FR-09 | File contains `0` | `DivideByZeroException` is reported |
| FR-10 | File name is blank | `ArgumentException` is reported |
| FR-11 | Processing finishes or fails | The file can be reopened or deleted afterward |
| FR-12 | Processing succeeds or fails | The console reports the outcome and finishes normally |
| FR-13 | The parent directory does not exist | `DirectoryNotFoundException` is reported |
| FR-14 | The supplied path is a directory rather than a readable file | `UnauthorizedAccessException` is reported |
| FR-15 | Another process or stream holds an exclusive file lock | `IOException` is reported |
| FR-16 | Output writer is null | `ArgumentNullException` is reported |
| FR-17 | Output writer is already disposed | `ObjectDisposedException` is reported |
| FR-18 | File path is longer than the platform supports | `PathTooLongException` is reported |

## Definition of done

- [ ] Every acceptance criterion has an automated test.
- [ ] All automated tests pass.
- [ ] The solution builds without errors.
- [ ] The file-processing code can be tested without starting the console application.
- [ ] No test depends on the developer's local file system state.

