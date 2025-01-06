namespace Taskify.Identity.UseCases.ContextTypes.List;

using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;

public record ListContextTypesQuery() : IQuery<Result<IEnumerable<ListContextTypeDto>>>;
