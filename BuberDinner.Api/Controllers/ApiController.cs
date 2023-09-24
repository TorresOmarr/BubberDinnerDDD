using BuberDinner.Api.Commons.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
public class ApiController: ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemsKey.Errors] = errors;
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Validation => 400,
            _ => 500
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}