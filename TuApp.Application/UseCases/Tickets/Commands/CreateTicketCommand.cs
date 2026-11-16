using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Tickets.Commands;

public record CreateTicketCommand(string UserId, string Title, string? Description) : IRequest<string>;

internal sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, string>
{
    private readonly IGenericRepository<Ticket> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketCommandHandler(IGenericRepository<Ticket> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = Guid.Parse(request.UserId),
            Title = request.Title,
            Description = request.Description,
            Status = "abierto",
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.TicketId.ToString();
    }
}
