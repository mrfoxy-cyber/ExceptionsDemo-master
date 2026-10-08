# Use Case: Process a Number File

> Calculate 100 divided by a whole number stored on the first line of a text file.

## Overview

| Field | Description |
|---|---|
| **ID** | UC-001 |
| **Status** | Draft |
| **Priority** | Must |
| **Primary actor** | Application user |
| **Supporting actors** | Local file system |
| **Goal** | Receive a calculated result or a clear explanation of why the file could not be processed |
| **Trigger** | The user starts the console application |

## Scope

### In scope

- Locate `numbers.txt` in the execution directory.
- Read and parse the first line.
- Calculate `100.0 / number`.
- Handle expected file, format, overflow, empty-file, and zero-value errors.
- Close the file reader after processing.

### Out of scope

- Processing more than one line.
- Asking the user to select a file.
- Modifying the contents of the file.
- Retrying automatically after an error.

## Preconditions

- The application can access its execution directory.
- When present, `numbers.txt` is readable by the application.

## Postconditions

### On success

- The calculated result is displayed.
- The file reader is closed.
- The application finishes normally.

### On failure

- A specific error message is displayed.
- No calculation result is displayed.
- The file reader is closed if it was opened.
- The application finishes normally.

## Main flow

1. The user starts the application.
2. The system builds the path to `numbers.txt`.
3. The system opens the file.
4. The system reads the first line.
5. The system parses the line as a 32-bit whole number.
6. The system verifies that the number is not zero.
7. The system calculates `100.0 / number`.
8. The system closes the file reader.
9. The system displays the result.
10. The application finishes normally.

## Error flows

### E1 — File does not exist

Begins at step 3 of the main flow.

1. The file system reports that the file does not exist.
2. The system displays a file-not-found message.
3. The application finishes normally.

### E2 — File is empty

Begins at step 4 of the main flow.

1. The system receives no first line.
2. The system raises an `InvalidOperationException`.
3. The system closes the file reader.
4. The application displays an empty-file error.

### E3 — First line is not a whole number

Begins at step 5 of the main flow.

1. Parsing raises a `FormatException`.
2. The system closes the file reader.
3. The application displays a format error.

### E4 — First line is outside the integer range

Begins at step 5 of the main flow.

1. Parsing raises an `OverflowException`.
2. The system closes the file reader.
3. The application displays an overflow error.

### E5 — First line is zero

Begins at step 6 of the main flow.

1. The system raises a `DivideByZeroException`.
2. The system closes the file reader.
3. The application displays a divide-by-zero error.

## Acceptance criteria

### AC-01 — Valid whole number

**Given** a readable number file whose first line is `4`  
**When** the file is processed  
**Then** the result is `25`  
**And** the file reader is closed

### AC-02 — Decimal result

**Given** a readable number file whose first line is `8`  
**When** the file is processed  
**Then** the result is `12.5`

### AC-03 — Missing file

**Given** the supplied file path does not exist  
**When** the file is processed  
**Then** a `FileNotFoundException` is produced

### AC-04 — Empty file

**Given** a readable but empty file  
**When** the file is processed  
**Then** an `InvalidOperationException` is produced  
**And** the error explains that the file is empty

### AC-05 — Invalid format

**Given** a readable number file whose first line is `hello`  
**When** the file is processed  
**Then** a `FormatException` is produced

### AC-06 — Number overflow

**Given** a readable number file whose first line is larger than `Int32.MaxValue`  
**When** the file is processed  
**Then** an `OverflowException` is produced

### AC-07 — Zero divisor

**Given** a readable number file whose first line is `0`  
**When** the file is processed  
**Then** a `DivideByZeroException` is produced  
**And** its message explains that division by zero is not allowed

### AC-08 — Blank file name

**Given** a blank file name  
**When** file processing is requested  
**Then** an `ArgumentException` is produced  
**And** its parameter name is `fileName`

### AC-09 — Application completes after an error

**Given** a readable number file whose first line is `0`  
**When** the console application processes the file  
**Then** a divide-by-zero error is displayed  
**And** the application reports that it finished normally

## Test coverage

| Test | Acceptance criterion | Expected result |
|---|---|---|
| `ProcessFile_WhenFileContainsFour_ReturnsTwentyFive` | AC-01 | Returns `25` |
| `ProcessFile_WhenResultIsFractional_PreservesDecimalValue` | AC-02 | Returns `12.5` |
| `ProcessFile_WhenFileDoesNotExist_ThrowsFileNotFoundException` | AC-03 | Throws the specific exception |
| `ProcessFile_WhenFileIsEmpty_ThrowsInvalidOperationException` | AC-04 | Reports an empty file |
| `ProcessFile_WhenContentIsNotNumeric_ThrowsFormatException` | AC-05 | Throws the specific exception |
| `ProcessFile_WhenNumberIsTooLarge_ThrowsOverflowException` | AC-06 | Throws the specific exception |
| `ProcessFile_WhenNumberIsZero_ThrowsDivideByZeroException` | AC-07 | Rejects zero |
| `ProcessFile_WhenFileNameIsBlank_ThrowsArgumentException` | AC-08 | Rejects the file name |
| `Run_WhenProcessingFails_ReportsErrorAndFinishesNormally` | AC-09 | Handles the error and completes |

## Implementation note

`ProcessFile` is currently a local function inside `Main`. To test it directly, move it to an accessible class or make it an accessible method before implementing the automated tests.

## Definition of done

- [ ] AC-01 through AC-09 are covered by automated tests.
- [ ] Expected exception types are preserved for the caller.
- [ ] Temporary test files are isolated and cleaned up.
- [ ] The solution builds without errors.
- [ ] All automated tests pass.
