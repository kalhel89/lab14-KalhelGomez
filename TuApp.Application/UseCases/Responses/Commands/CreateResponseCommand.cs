using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Responses.Commands;

public record CreateResponseCommand : IRequest<string>
{
    public string TicketId { get; set; } = null!;
    public string ResponderId { get; set; } = null!;
    public string Message { get; set; } = null!;
}

internal sealed class CreateResponseCommandHandler : IRequestHandler<CreateResponseCommand, string>
{
    private readonly IGenericRepository<Response> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateResponseCommandHandler(IGenericRepository<Response> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = Guid.Parse(request.TicketId),
            ResponderId = Guid.Parse(request.ResponderId),
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(response);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return response.ResponseId.ToString();
    }
}
