using ClosedXML.Excel;
using TuApp.Application.Services;
using TuApp.Domain.Entities;
using TuApp.Domain.Repositories;

namespace TuApp.Infrastructure.Services;

public sealed class ExcelReportService(
    IGenericRepository<Ticket> ticketRepository,
    IGenericRepository<User> userRepository) : IExcelReportService
{
    public async Task<byte[]> GenerateTicketsReportAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var tickets = await ticketRepository.GetAllAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Tickets");
        worksheet.Cell(1, 1).InsertTable(tickets.Select(ticket => new
        {
            TicketId = ticket.TicketId,
            UserId = ticket.UserId,
            ticket.Title,
            ticket.Description,
            ticket.Status,
            ticket.CreatedAt,
            ticket.ClosedAt
        }), "TicketsReport");

        FormatReport(worksheet);
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerateUsersReportAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var users = await userRepository.GetAllAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Usuarios");
        worksheet.Cell(1, 1).InsertTable(users.Select(user => new
        {
            UserId = user.UserId,
            user.Username,
            user.Email,
            user.CreatedAt
        }), "UsersReport");

        FormatReport(worksheet);
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void FormatReport(IXLWorksheet worksheet)
    {
        worksheet.SheetView.FreezeRows(1);
        worksheet.Columns().AdjustToContents();
    }
}
