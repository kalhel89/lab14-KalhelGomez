using MediatR;
using TuApp.Application.Dtos;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Tickets.Queries;

public record GetTicketByIdQuery(string TicketId) : IRequest<TicketDto?>;

internal sealed class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
{
    private readonly IGenericRepository<Ticket> _repository;

    public GetTicketByIdQueryHandler(IGenericRepository<Ticket> repository) => _repository = repository;

    public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _repository.GetByIdAsync(request.TicketId);
        return t is null
            ? null
            : new TicketDto(
                t.TicketId.ToString(),
                t.UserId.ToString(),
                t.Title,
                t.Description,
                t.Status,
                t.CreatedAt,
                t.ClosedAt);
    }
}
