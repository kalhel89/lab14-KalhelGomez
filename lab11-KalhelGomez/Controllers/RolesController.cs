using MediatR;
using Microsoft.AspNetCore.Mvc;
using TuApp.Application.UseCases.Roles.Commands;
using TuApp.Application.UseCases.Roles.Queries;

namespace lab11_KalhelGomez.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ISender _mediator;
    public RolesController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetRolesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { RoleId = id });
    }
}
