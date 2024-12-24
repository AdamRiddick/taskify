namespace Taskify.SharedKernel.Configuration
{
    public interface ITaskifyEnvironmentSettings
    {
        string EnvironmentName { get; }
        bool IsDevelopment { get; }
    }
}