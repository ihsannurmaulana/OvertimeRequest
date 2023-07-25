using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations;

public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidation()
    {
        RuleFor(p => p.Email)
           .NotEmpty()
           .EmailAddress();
    }
}