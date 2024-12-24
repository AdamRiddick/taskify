namespace Taskify.SharedKernel.Data;

/// <summary>
/// Apply this marker interface only to aggregate root entities.
/// The repository implementation uses constraints to ensure it only operates on aggregate roots
/// </summary>
public interface IAggregateRoot { }
