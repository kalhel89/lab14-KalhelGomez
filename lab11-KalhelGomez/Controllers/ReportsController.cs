using MediatR;
using Microsoft.AspNetCore.Mvc;
using TuApp.Application.UseCases.Tickets.Queries;
using TuApp.Application.UseCases.Users.Queries;

namespace lab11_KalhelGomez.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ReportsController(ISender mediator) : ControllerBase
{
    private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    /// <summary>Descarga el reporte de tickets en formato Excel.</summary>
    [HttpGet("tickets/excel")]
    [Produces(ExcelContentType)]
    public async Task<IActionResult> GetTicketsExcel(CancellationToken cancellationToken)
    {
        var content = await mediator.Send(new GetTicketsExcelReportQuery(), cancellationToken);
        return File(content, ExcelContentType, "reporte-tickets.xlsx");
    }

    /// <summary>Descarga el reporte de usuarios en formato Excel.</summary>
    [HttpGet("users/excel")]
    [Produces(ExcelContentType)]
    public async Task<IActionResult> GetUsersExcel(CancellationToken cancellationToken)
    {
        var content = await mediator.Send(new GetUsersExcelReportQuery(), cancellationToken);
        return File(content, ExcelContentType, "reporte-usuarios.xlsx");
    }
}
