using MediatR;
using TuApp.Application.Dtos;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Tickets.Queries;

public record GetTicketsQuery : IRequest<IEnumerable<TicketDto>>;

internal sealed class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IEnumerable<TicketDto>>
{
    private readonly IGenericRepository<Ticket> _repository;

    public GetTicketsQueryHandler(IGenericRepository<Ticket> repository) => _repository = repository;

    public async Task<IEnumerable<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _repository.GetAllAsync();
        return tickets.Select(t => new TicketDto(
            t.TicketId.ToString(),
            t.UserId.ToString(),
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.ClosedAt));
    }
}
