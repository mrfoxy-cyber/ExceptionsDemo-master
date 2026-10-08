using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace ExceptionDemo.Tests;

public sealed partial class SpecificationTraceabilityTests
{
    [Fact]
    public void EveryDocumentedRequirementAndAcceptanceCriterion_HasLinkedTest()
    {
        HashSet<string> documentedRequirements = ReadIds(
            "Requirements.md",
            RequirementIdPattern());
        HashSet<string> documentedCriteria = ReadIds(
            "UseCase-ProcessNumberFile.md",
            AcceptanceCriterionIdPattern());
        IReadOnlyList<MethodInfo> testMethods = GetTraceableTestMethods();

        HashSet<string> linkedRequirements = ReadTraits(
            testMethods,
            Traceability.Requirement);
        HashSet<string> linkedCriteria = ReadTraits(
            testMethods,
            Traceability.AcceptanceCriterion);

        Assert.Empty(documentedRequirements.Except(linkedRequirements));
        Assert.Empty(documentedCriteria.Except(linkedCriteria));
        Assert.Empty(linkedRequirements.Except(documentedRequirements));
        Assert.Empty(linkedCriteria.Except(documentedCriteria));
    }

    [Fact]
    public void EveryAcceptanceTest_HasRequirementUseCaseAndCriterionTraits()
    {
        foreach (MethodInfo method in GetTraceableTestMethods())
        {
            HashSet<string> traitNames = method
                .CustomAttributes
                .Where(attribute => attribute.AttributeType == typeof(TraitAttribute))
                .Select(attribute =>
                    (string)attribute.ConstructorArguments[0].Value!)
                .ToHashSet();

            Assert.Contains(Traceability.Requirement, traitNames);
            Assert.Contains(Traceability.UseCase, traitNames);
            Assert.Contains(Traceability.AcceptanceCriterion, traitNames);
        }
    }

    private static HashSet<string> ReadIds(string fileName, Regex pattern)
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            "Specifications",
            fileName);
        string specification = File.ReadAllText(path);

        return pattern.Matches(specification)
            .Select(match => match.Groups[1].Value)
            .ToHashSet();
    }

    private static IReadOnlyList<MethodInfo> GetTraceableTestMethods() =>
        typeof(SpecificationTraceabilityTests).Assembly
            .GetTypes()
            .SelectMany(type => type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static))
            .Where(method => method
                .CustomAttributes
                .Where(attribute => attribute.AttributeType == typeof(TraitAttribute))
                .Select(attribute =>
                    (string)attribute.ConstructorArguments[0].Value!)
                .Any(name =>
                    name == Traceability.Requirement ||
                    name == Traceability.AcceptanceCriterion))
            .ToList();

    private static HashSet<string> ReadTraits(
        IEnumerable<MethodInfo> methods,
        string traitName) =>
        methods
            .SelectMany(method => method.CustomAttributes)
            .Where(attribute =>
                attribute.AttributeType == typeof(TraitAttribute) &&
                (string)attribute.ConstructorArguments[0].Value! == traitName)
            .Select(attribute =>
                (string)attribute.ConstructorArguments[1].Value!)
            .ToHashSet();

    [GeneratedRegex(@"^\|\s*(FR-\d+)\s*\|", RegexOptions.Multiline)]
    private static partial Regex RequirementIdPattern();

    [GeneratedRegex(@"^###\s+(AC-\d+)\b", RegexOptions.Multiline)]
    private static partial Regex AcceptanceCriterionIdPattern();
}
