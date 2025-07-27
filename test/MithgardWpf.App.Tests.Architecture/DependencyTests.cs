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

    [Fact]
    public void Common_ShouldNotDependOnCore()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(typeof(AppViewModel).Assembly)
            .That()
            .ResideInNamespace("MithgardWpf.App.Core")
            .ShouldNot()
            .HaveDependencyOn("MithgardWpf.App.Common")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "These classes in Core reference Common:\n" + string.Join("\n", result.FailingTypeNames));
    }
}
