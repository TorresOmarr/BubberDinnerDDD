using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Commons;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{

    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        return authResult.Match(
           result => Ok(MapAuthResult(result)),
           errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.user.Id,
                                                   authResult.user.FirstName,
                                                   authResult.user.LastName,
                                                   authResult.user.Email,
                                                   authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationQueryService.Login(request.Email, request.Password);
        return authResult.Match(
          result => Ok(MapAuthResult(result)),
          errors => Problem(errors)
       );
    }
}