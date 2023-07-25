using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(p => p.Email)
           .NotEmpty()
           .EmailAddress();

        RuleFor(p => p.Password)
           .NotEmpty();
    }
}
