using MediatR;
using TuApp.Application.Dtos;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Application.UseCases.Responses.Queries;

public record GetResponsesQuery : IRequest<IEnumerable<ResponseDto>>;

internal sealed class GetResponsesQueryHandler : IRequestHandler<GetResponsesQuery, IEnumerable<ResponseDto>>
{
    private readonly IGenericRepository<Response> _repository;

    public GetResponsesQueryHandler(IGenericRepository<Response> repository) => _repository = repository;

    public async Task<IEnumerable<ResponseDto>> Handle(GetResponsesQuery request, CancellationToken cancellationToken)
    {
        var responses = await _repository.GetAllAsync();
        return responses.Select(r => new ResponseDto(
            r.ResponseId.ToString(),
            r.TicketId.ToString(),
            r.ResponderId.ToString(),
            r.Message,
            r.CreatedAt));
    }
}
