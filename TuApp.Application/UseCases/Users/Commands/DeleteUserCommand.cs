using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Users.Commands;

public record DeleteUserCommand(string UserId) : IRequest<bool>;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IGenericRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IGenericRepository<User> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId);
        if (user is null) return false;
        await _repository.DeleteAsync(request.UserId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
