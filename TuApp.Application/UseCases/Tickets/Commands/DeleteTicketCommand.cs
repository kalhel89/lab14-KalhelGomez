using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Tickets.Commands;

public record DeleteTicketCommand(string TicketId) : IRequest<bool>;

internal sealed class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
{
    private readonly IGenericRepository<Ticket> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketCommandHandler(IGenericRepository<Ticket> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId);
        if (ticket is null) return false;

        await _repository.DeleteAsync(request.TicketId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
