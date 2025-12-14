using ServiceDesk.Application.Common.Interfaces;
using ServiceDesk.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Application.Features.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result<LoginResponse>.Failure("Credenciais inválidas.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            return Result<LoginResponse>.Failure("Credenciais inválidas.");

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateToken(user, roles);

        var response = new LoginResponse(user.Id.ToString(), user.FullName, user.Email!, token);

        return Result<LoginResponse>.Success(response);
    }
}