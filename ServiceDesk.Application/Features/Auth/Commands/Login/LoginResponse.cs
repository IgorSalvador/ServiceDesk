namespace ServiceDesk.Application.Features.Auth.Commands.Login;

public record LoginResponse(
        string Id,
        string FullName,
        string Email,
        string Token
    );
