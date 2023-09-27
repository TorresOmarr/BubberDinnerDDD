using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Commons;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Errors;
using Microsoft.AspNetCore.Authorization;

namespace BuberDinner.Api.Controllers;


[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
        return authResult.Match(
           result => Ok(_mapper.Map<AuthenticationResponse>(result)),
           errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                detail: authResult.FirstError.Description);
        }
        return authResult.Match(
          result => Ok(_mapper.Map<AuthenticationResponse>(result)),
          errors => Problem(errors)
       );
    }
}