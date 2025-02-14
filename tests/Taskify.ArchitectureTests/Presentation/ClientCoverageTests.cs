namespace Taskify.ArchitectureTests.Common;

using System;
using System.Collections.Generic;
using System.Reflection;

using Xunit;

public class ClientCoverageTests
{
    [Fact]
    public void client_coverage_check()
    {
        // Arrange
        var localPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;

        // Navigate up to the desired directory level
        DirectoryInfo directoryInfo = new DirectoryInfo(localPath);
        for (int i = 0; i < 6; i++)
        {
            directoryInfo = directoryInfo.Parent ?? throw new InvalidOperationException("Cannot navigate up the directory structure. Path too short.");
        }

        var basePath = directoryInfo.FullName;
        var clientsPath = Path.Combine(basePath, "src", "Presentation");
        var archTestsPath = Path.Combine(basePath, "tests", "Taskify.ArchitectureTests", "Presentation");

        // Act
        var missingClientTests = new List<string>();
        foreach (var folder in Directory.GetDirectories(clientsPath))
        {
            if (folder.EndsWith("Components") || folder.EndsWith("Taskify.App")) continue;
            var clientName = Path.GetFileName(folder).Replace("Taskify.", "");
            var className = $"{clientName}Tests";

            if (!File.Exists(Path.Combine(archTestsPath, $"{className}.cs")))
            {
                missingClientTests.Add(Path.GetFileName(clientName));
            }
        }

        // Assert
        Assert.Empty(missingClientTests);
    }
}
