using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Application.Common.Models;
using ServiceDesk.Application.Features.Auth.Commands.Login;

namespace ServiceDesk.Api.Controllers;

public class AuthController : BaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await Mediator.Send(command, ct);

        return HandleResult(result);
    }
}
