using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Tickets.Commands;

public record UpdateTicketCommand(string TicketId, string Title, string? Description, string Status) : IRequest<bool>;

internal sealed class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, bool>
{
    private readonly IGenericRepository<Ticket> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketCommandHandler(IGenericRepository<Ticket> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId);
        if (ticket is null) return false;

        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.Status = request.Status;
        if (request.Status == "cerrado") ticket.ClosedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
