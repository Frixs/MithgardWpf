// Author: Tomáš Frixs

namespace MithgardWpf.App.Tests.Architecture;

public class XamlDependencyTests
{
    private readonly string _projectRootPath;

    #region Setup & Teardown

    // Setup goes here
    public XamlDependencyTests()
    {
        string projectNamespace = "MithgardWpf.App";
        _projectRootPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\..\\src\\{projectNamespace}"));
    }

    #endregion

    [Theory]
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
        string forbiddenClrNamespace = $"clr-namespace:{forbiddenNamespace}";
        var xamlFiles = Directory.GetFiles(_projectRootPath, "*.xaml", SearchOption.AllDirectories);

        // Act
        var offenders = xamlFiles
            .Where(file =>
            {
                var content = File.ReadAllText(file);
                return content.Contains($"x:Class=\"{resideNamespace}") &&
                       content.Contains(forbiddenClrNamespace);
            })
            .ToList();

        // Assert
        Assert.True(offenders.Count == 0, $"Dependency on {forbiddenNamespace} should not exist in:\n" + string.Join("\n", offenders));
    }
}
