using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using System.Security.Claims;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    private readonly OvertimeDbContext _context;

    private readonly IEmployeeRepository _employeeRepository;

    private readonly IRoleRepository _roleRepository;

    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmailHandler _emailHandler;

    private readonly ITokenHandler _tokenHandler;

    public AccountService(IAccountRepository accountRepository, OvertimeDbContext context, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, ITokenHandler tokenHandler, IEmailHandler emailHandler)
    {
        _accountRepository = accountRepository;
        _context = context;
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _tokenHandler = tokenHandler;
        _emailHandler = emailHandler;
    }

    public bool RegistrationAccount(RegisterDto registerDto)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            Employee employeeData = new NewEmployeeDto
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                PhoneNumber = registerDto.PhoneNumber,
                ManagerGuid = registerDto.ManagerGuid
            };
            employeeData.Nik = GenerateHandler.Nik(_employeeRepository.GetLastEmployeeNik());

            var employee = _employeeRepository.Create(employeeData);

            var account = _accountRepository.Create(new NewAccountDto
            {
                Guid = employeeData.Guid,
                Email = registerDto.Email,
                Password = HashingHandler.Hash(registerDto.Password)
            });

            var getRoleUser = _roleRepository.GetByName("User");
            _accountRoleRepository.Create(new NewAccountRoleDto
            {
                AccountGuid = account.Guid,
                RoleGuid = getRoleUser.Guid
            });

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public string LoginAccount(LoginDto login)
    {
        var employee = _accountRepository.GetEmployeeByEmail(login.Email);
        if (employee is null) return "0";

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return "0";

        if (!HashingHandler.Validate(login.Password, account!.Password))
            return "-1";

        var employees = _employeeRepository.GetByGuid(employee.Guid);
        try
        {
            var claims = new List<Claim>() {
                new Claim("NIK", employees.Nik),
                new Claim("FullName", $"{employees.FirstName} {employees.LastName}"),
                new Claim("EmailAddress", login.Email)
            };

            var getAccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(employee.Guid);
            var getRoleNameByAccountRole = from ar in getAccountRole
                                           join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                           select r.Name;

            claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var getToken = _tokenHandler.GenerateToken(claims);
            return getToken;
        }
        catch
        {
            return "-2";
        }
    }

    // Forgot Password
    public ForgotPasswordDto ForgotPassword(string email)
    {
        var account = _accountRepository.GetEmployeeByEmail(email);
        if (account is null)
        {
            return null;
        }

        var toDto = new ForgotPasswordDto
        {
            Email = account.Email,
            Otp = GenerateHandler.OtpNumber(),
            ExpiredTime = DateTime.Now.AddMinutes(5)
        };

        var relatedAccount = _accountRepository.GetByGuid(account.Guid);

        var update = new Account
        {
            Guid = relatedAccount.Guid,
            Email = relatedAccount.Email,
            Password = relatedAccount.Password,
            Otp = toDto.Otp,
            IsActive = relatedAccount.IsActive,
            IsUsed = relatedAccount.IsUsed,
            ExpiredTime = DateTime.Now.AddMinutes(5)

        };

        var updateResult = _accountRepository.Update(update);

        if (!updateResult)
        {
            return null;
        }

        _emailHandler.SendEmail(toDto.Email,
                                "Forgot Password",
                                $"Your OTP is {toDto.Otp}");

        return toDto;
    }


    public IEnumerable<GetAccountDto> GetAccount()
    {
        var accounts = _accountRepository.GetAll().ToList();
        if (!accounts.Any()) return Enumerable.Empty<GetAccountDto>();
        List<GetAccountDto> accountDtos = new();

        foreach (var account in accounts)
        {
            accountDtos.Add((GetAccountDto)account);
        }

        return accountDtos;
    }

    public GetAccountDto? GetAccountByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null) return null;

        return (GetAccountDto)account;
    }

    public GetAccountDto? CreateAccount(NewAccountDto newAccountDto)
    {
        var createdAccount = _accountRepository.Create(newAccountDto);
        if (createdAccount is null) return null;

        return (GetAccountDto)createdAccount;
    }

    public int UpdateAccount(UpdateAccountDto updateAccountDto)
    {
        var getAccount = _accountRepository.GetByGuid(updateAccountDto.Guid);

        if (getAccount is null) return -1;

        var isUpdate = _accountRepository.Update(updateAccountDto);
        return !isUpdate ? 0 :
            1;
    }

    public int DeleteAccount(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);

        if (account is null) return -1;

        var isDelete = _accountRepository.Delete(account);
        return !isDelete ? 0 :
            1;
    }
}
