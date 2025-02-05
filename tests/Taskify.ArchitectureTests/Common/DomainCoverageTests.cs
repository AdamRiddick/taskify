namespace Taskify.ArchitectureTests.Common;

using System;
using System.Collections.Generic;
using System.Reflection;

using Xunit;

public class DomainCoverageTests
{
    [Fact]
    public void domain_coverage_check()
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
        var domainsPath = Path.Combine(basePath, "src", "Domains");
        var archTestsPath = Path.Combine(basePath, "tests", "Taskify.ArchitectureTests", "Domains");

        // Act
        var missingDomainTests = new List<string>();
        foreach (var folder in Directory.GetDirectories(domainsPath))
        {
            var domainName = Path.GetFileName(folder);
            var className = $"{domainName}Tests";

            if (!File.Exists(Path.Combine(archTestsPath, $"{className}.cs")))
            {
                missingDomainTests.Add(Path.GetFileName(domainName));
            }
        }

        // Assert
        Assert.Empty(missingDomainTests);
    }
}
