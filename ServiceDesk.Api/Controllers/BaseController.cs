using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result);
    }
}
