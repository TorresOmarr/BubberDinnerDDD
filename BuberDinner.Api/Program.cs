using BuberDinner.Api;
using BuberDinner.Api.Commons.Errors;
using BuberDinner.Api.Commons.Mapping;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);





}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");


    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}
