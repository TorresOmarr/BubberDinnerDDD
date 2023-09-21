using BuberDinner.Api.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.Map("/error", (HttpContext httpContext) =>{
        Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    });
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}
