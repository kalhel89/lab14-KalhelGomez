namespace TuApp.Application.Services;

public interface IExcelReportService
{
    Task<byte[]> GenerateTicketsReportAsync(CancellationToken cancellationToken = default);
    Task<byte[]> GenerateUsersReportAsync(CancellationToken cancellationToken = default);
}
