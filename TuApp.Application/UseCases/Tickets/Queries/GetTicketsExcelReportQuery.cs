using MediatR;
using TuApp.Application.Services;

namespace TuApp.Application.UseCases.Tickets.Queries;

public sealed record GetTicketsExcelReportQuery : IRequest<byte[]>;

internal sealed class GetTicketsExcelReportQueryHandler(IExcelReportService reportService)
    : IRequestHandler<GetTicketsExcelReportQuery, byte[]>
{
    public Task<byte[]> Handle(GetTicketsExcelReportQuery request, CancellationToken cancellationToken) =>
        reportService.GenerateTicketsReportAsync(cancellationToken);
}
