using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Users.Commands;

public record RegisterUserCommand(string Username, string Password, string? Email) : IRequest<string>;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IGenericRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IGenericRepository<User> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.Password,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.UserId.ToString();
    }
}
