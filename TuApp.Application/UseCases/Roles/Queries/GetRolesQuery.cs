using MediatR;
using TuApp.Application.Dtos;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Roles.Queries;

public record GetRolesQuery : IRequest<IEnumerable<RoleDto>>;

internal sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IGenericRepository<Role> _repository;

    public GetRolesQueryHandler(IGenericRepository<Role> repository) => _repository = repository;

    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _repository.GetAllAsync();
        return roles.Select(r => new RoleDto(r.RoleId.ToString(), r.RoleName));
    }
}
