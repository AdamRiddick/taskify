namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.xUnit;

using System;
using System.Collections.Generic;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

using Assembly = System.Reflection.Assembly;

/// <remarks>
/// This class is generic to take advantage of the fact that static members are not shared between
/// derived types. For performance, we want to share the static members across instances of a derived type 
/// but kept unique per derived type.
/// See here: https://stackoverflow.com/a/49582829/295813
/// Because of this, we've had to disable some warnings below.
/// </remarks>
#pragma warning disable S2326 // Unused type parameters should be removed
public abstract class ClientTestsBase<T> : TestBase<T> where T : class
#pragma warning disable S2326 // Unused type parameters should be removed
{
#pragma warning disable S2743 // Static fields should not be used in generic types
    private static bool _initialised = false;

    protected static List<string> _additionalReferences = [];

    protected static string _client = string.Empty;
#pragma warning restore S2743 // Static fields should not be used in generic types

    /// <summary>
    /// Constructor
    /// </summary>
    /// <exception cref="Exception">Ensure Init() has been called in derived class static constructor.</exception>
    protected ClientTestsBase()
        : base()
    {
        if (!_initialised)
        {
            throw new Exception("Client has not been initialised. Call ClientTestsBase.Init(client, assemblies)");
        }
    }

    protected static void Init(
        string client,
        Assembly[] assemblies,
        List<string>? additionalReferences = null)
    {
        _client = client;
        if (additionalReferences != null) _additionalReferences = additionalReferences;
        Init(assemblies);
        _initialised = true;
    }

    [Fact]
    public void dependencies_check()
    {
        var namespaceToTest = $"Taskify.{_client}";
        var allowedReferences = new List<string>() {
            namespaceToTest,
            "Taskify.Ui"
        };

        _additionalReferences.ForEach(allowedReferences.Add);

        var rejectedReferences = new List<string>() {
            "\bTest\b"
        };

        if (!_client.Equals("Api")) rejectedReferences.Add("Taskify.Api");

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(BuildDependencies(allowedReferences), true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }

    [Fact]
    public void should_not_contain_razor_or_blazor_components_check()
    {
        var rule = Types()
                    .Should()
                    .NotBeAssignableTo(typeof(Microsoft.AspNetCore.Mvc.RazorPages.PageModel))
                    .AndShould()
                    .NotBeAssignableTo(typeof(Microsoft.AspNetCore.Components.ComponentBase));

        rule.Check(Architecture);
    }
}
