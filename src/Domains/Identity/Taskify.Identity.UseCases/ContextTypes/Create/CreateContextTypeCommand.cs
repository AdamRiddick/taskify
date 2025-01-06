namespace Taskify.Identity.UseCases.ContextTypes.Create;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record CreateContextTypeCommand(CreateContextTypeDto Dto) : ICommand<Result<int>>;