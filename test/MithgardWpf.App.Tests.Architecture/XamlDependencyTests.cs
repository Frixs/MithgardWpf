namespace MithgardWpf.App.Tests.Architecture;

public class XamlDependencyTests
{
    [Fact]
    public void Common_ShouldNotDependOnCore()
    {
        // Arrange
        string project = "MithgardWpf.App";
        string searchedNamespace = $"{project}.Core";
        string forbiddenClrNamespace = $"clr-namespace:{project}.Common";
        var projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\..\\src\\{project}"));
        var xamlFiles = Directory.GetFiles(projectRoot, "*.xaml", SearchOption.AllDirectories);

        // Act
        var offenders = xamlFiles
            .Where(file =>
            {
                var content = File.ReadAllText(file);
                return content.Contains($"x:Class=\"{searchedNamespace}") &&
                       content.Contains(forbiddenClrNamespace);
            })
            .ToList();

        // Assert
        Assert.True(offenders.Count == 0, "These XAML files in Core reference Common:\n" + string.Join("\n", offenders));
    }
}
