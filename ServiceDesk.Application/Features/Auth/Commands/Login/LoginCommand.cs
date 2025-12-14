using MediatR;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;