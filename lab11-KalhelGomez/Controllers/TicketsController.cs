using MediatR;
using Microsoft.AspNetCore.Mvc;
using TuApp.Application.UseCases.Tickets.Commands;
using TuApp.Application.UseCases.Tickets.Queries;

namespace lab11_KalhelGomez.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ISender _mediator;
    public TicketsController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetTicketsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetTicketByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { TicketId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateTicketCommand command)
    {
        if (id != command.TicketId) return BadRequest("ID mismatch");
        var updated = await _mediator.Send(command);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _mediator.Send(new DeleteTicketCommand(id));
        return deleted ? NoContent() : NotFound();
    }
}
