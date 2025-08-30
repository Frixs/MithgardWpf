// Author: Tomáš Frixs

using MithgardWpf.App.Core.ViewModels;
using NetArchTest.Rules;

namespace MithgardWpf.App.Tests.Architecture;

public class DependencyTests
{
    [Theory]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.Core")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.FeaturesInfrastructure")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.FeaturesShared")]
    [InlineData("MithgardWpf.App.Common", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.FeaturesInfrastructure")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.FeaturesShared")]
    [InlineData("MithgardWpf.App.Core", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.Features", "MithgardWpf.App.FeaturesInfrastructure")]
    [InlineData("MithgardWpf.App.Features", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.FeaturesInfrastructure", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.FeaturesInfrastructure", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.FeaturesShared", "MithgardWpf.App.Features")]
    [InlineData("MithgardWpf.App.FeaturesShared", "MithgardWpf.App.FeaturesInfrastructure")]
    [InlineData("MithgardWpf.App.FeaturesShared", "MithgardWpf.App.Pages")]
    [InlineData("MithgardWpf.App.Pages", "MithgardWpf.App.FeaturesInfrastructure")]
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
