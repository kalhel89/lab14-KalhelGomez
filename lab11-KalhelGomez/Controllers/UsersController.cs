using MediatR;
using Microsoft.AspNetCore.Mvc;
using TuApp.Application.UseCases.Users.Commands;
using TuApp.Application.UseCases.Users.Queries;

namespace lab11_KalhelGomez.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;
    public UsersController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetUsersQuery()));

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { UserId = id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _mediator.Send(new DeleteUserCommand(id));
        return deleted ? NoContent() : NotFound();
    }
}
