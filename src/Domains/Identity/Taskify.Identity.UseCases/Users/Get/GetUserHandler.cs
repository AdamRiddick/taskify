namespace Taskify.Identity.UseCases.Users.Get;

using Ardalis.Result;

using Mapster;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class GetUserHandler : IQueryHandler<GetUserQuery, Result<GetUserDto>>
{
    private readonly IReadRepository<User> _repository;

    public GetUserHandler(IReadRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if(item == null)
            return Result.NotFound("User not found.");

        var dto = item.Adapt<GetUserDto>(); 
        return new Result<GetUserDto>(dto);
    }
}
