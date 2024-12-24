namespace Taskify.SharedKernel.Configuration;

public class TaskifyEnvironmentSettings(string environmentName) : ITaskifyEnvironmentSettings
{
    public string EnvironmentName { get; } = environmentName;

    public bool IsDevelopment => EnvironmentName == "Development";

}
