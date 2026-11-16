using MediatR;
using Microsoft.AspNetCore.Mvc;
using TuApp.Application.UseCases.Responses.Commands;
using TuApp.Application.UseCases.Responses.Queries;

namespace lab11_KalhelGomez.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponsesController : ControllerBase
{
    private readonly ISender _mediator;
    public ResponsesController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetResponsesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { ResponseId = id });
    }
}
