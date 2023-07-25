using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations;

public class RegisterValidation : AbstractValidator<RegisterDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly IAccountRepository _accountRepository;

    public RegisterValidation(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;

        RuleFor(p => p.FirstName)
           .NotEmpty();

        RuleFor(p => p.BirthDate)
           .NotEmpty()
           .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

        RuleFor(p => p.Gender)
           .NotNull()
           .IsInEnum();

        RuleFor(p => p.HiringDate)
           .NotEmpty();

        RuleFor(p => p.Email)
           .NotEmpty()
           .Must(BeUniqueAccount).WithMessage(p => $"{p.Email} already registered")
           .EmailAddress();

        RuleFor(p => p.PhoneNumber)
           .NotEmpty()
           .Must(BeUniqueProperty).WithMessage(p => $"{p.PhoneNumber} already registered")
           .Matches(@"^\+[1-9]\d{1,20}$").WithMessage("Invalid phone number format.");

        RuleFor(p => p.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(p => p.ConfirmPassword)
          .NotEmpty()
          .Equal(model => model.Password).WithMessage("Password and Confirm Password do not match.");
        _accountRepository = accountRepository;
    }

    private bool BeUniqueProperty(string property)
    {
        return _employeeRepository.IsDuplicateValue(property);
    }

    private bool BeUniqueAccount(string property)
    {
        return _accountRepository.IsDuplicateValue(property);
    }
}
