using MediatR;
using TuApp.Application.Dtos;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Users.Queries;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IGenericRepository<User> _repository;

    public GetUsersQueryHandler(IGenericRepository<User> repository) => _repository = repository;

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync();
        return users.Select(u => new UserDto(u.UserId.ToString(), u.Username, u.Email, u.CreatedAt));
    }
}
