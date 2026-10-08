# Testing and Traceability

Each acceptance test is linked to the specification with three xUnit traits:

```csharp
[Trait(Traceability.Requirement, "FR-04")]
[Trait(Traceability.UseCase, "UC-001")]
[Trait(Traceability.AcceptanceCriterion, "AC-01")]
```

The traceability tests read `Requirements.md` and `UseCase-ProcessNumberFile.md` during the test run. They fail when:

- a documented requirement has no linked test;
- a documented acceptance criterion has no linked test;
- a test refers to an ID that is not in the documentation; or
- an acceptance test is missing one of the three traceability traits.

## Run tests by specification ID

Run all tests:

```powershell
dotnet test ExceptionsDemo.slnx
```

Run the tests for one requirement:

```powershell
dotnet test ExceptionsDemo.slnx --filter "Requirement=FR-09"
```

Run the tests for one acceptance criterion:

```powershell
dotnet test ExceptionsDemo.slnx --filter "AcceptanceCriterion=AC-07"
```

Run all tests belonging to the use case:

```powershell
dotnet test ExceptionsDemo.slnx --filter "UseCase=UC-001"
```

## Adding a new requirement

1. Add the new `FR-xx` row to `Requirements.md`.
2. Add or update an `AC-xx` scenario in the use-case document.
3. Add a test with the matching requirement, use-case, and acceptance-criterion traits.
4. Run the complete test suite.
