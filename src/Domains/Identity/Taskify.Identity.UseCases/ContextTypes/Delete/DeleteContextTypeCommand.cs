namespace Taskify.Identity.UseCases.ContextTypes.Delete;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record DeleteContextTypeCommand(int Id) : ICommand<Result>;
