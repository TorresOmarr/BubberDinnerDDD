using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login;


public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Introduzca un correo valido.");
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}