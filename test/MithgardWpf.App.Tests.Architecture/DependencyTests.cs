using MithgardWpf.App.Core.ViewModels;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace MithgardWpf.App.Tests;

public class DependencyTests : IDisposable
{
    private readonly ITestOutputHelper _output;

    // Setup goes here
    public DependencyTests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
    }

    // Teardown goes here
    public void Dispose()
    {
        // TODO https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests
        // Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
        // ITestOutputHelper, IAsyncLifetime
    }

    [Theory]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.Common")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.FeaturesShared")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.FeaturesShared")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.Features", "MithgardWpf.App.FeaturesShared")]
    [InlineData("MithgardWpf.App.Features", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.FeaturesShared", "MithgardWpf.App.Pages")]
    public void Namespace_ShouldNotDependOnNamespace(string resideNamespace, string forbiddenNamespace)
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(typeof(AppViewModel).Assembly)
            .That()
            .ResideInNamespace(resideNamespace)
            .ShouldNot()
            .HaveDependencyOn(forbiddenNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Dependency on {forbiddenNamespace} should not exist in:\n" + string.Join("\n", result.FailingTypeNames ?? []));
    }
}
