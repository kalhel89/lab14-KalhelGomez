using MediatR;
using TuApp.Application.Services;

namespace TuApp.Application.UseCases.Users.Queries;

public sealed record GetUsersExcelReportQuery : IRequest<byte[]>;

internal sealed class GetUsersExcelReportQueryHandler(IExcelReportService reportService)
    : IRequestHandler<GetUsersExcelReportQuery, byte[]>
{
    public Task<byte[]> Handle(GetUsersExcelReportQuery request, CancellationToken cancellationToken) =>
        reportService.GenerateUsersReportAsync(cancellationToken);
}
