using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
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
	private readonly IOvertimeRepository _overtimeRepository;

	private readonly IEmailHandler _emailHandler;

	private readonly ITokenHandler _tokenHandler;

	public AccountService(IAccountRepository accountRepository, OvertimeDbContext context, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, IOvertimeRepository overtimeRepository, IEmailHandler emailHandler, ITokenHandler tokenHandler)
	{
		_accountRepository = accountRepository;
		_context = context;
		_employeeRepository = employeeRepository;
		_roleRepository = roleRepository;
		_accountRoleRepository = accountRoleRepository;
		_overtimeRepository = overtimeRepository;
		_emailHandler = emailHandler;
		_tokenHandler = tokenHandler;
	}

	public bool RegistrationAccount(AccountDtoRegister registerDto)
	{
		using var transaction = _context.Database.BeginTransaction();
		try
		{
			var employee = new Employee
			{
				Guid = new Guid(),
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				BirthDate = registerDto.BirthDate,
				Gender = registerDto.Gender,
				HiringDate = registerDto.HiringDate,
				PhoneNumber = registerDto.PhoneNumber,
				ManagerGuid = registerDto.ManagerGuid,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now
			};
			employee.Nik = GenerateHandler.Nik(_employeeRepository.GetLastEmployeeNik());
			_employeeRepository.Create(employee);

			var account = new Account
			{
				Guid = employee.Guid,
				Email = registerDto.Email,
				Password = HashingHandler.Hash(registerDto.Password),
				IsActive = false,
				IsUsed = false,
				Otp = 0,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
				ExpiredTime = DateTime.Now.AddMinutes(10)
			};
			_accountRepository.Create(account);

			var roleEmployee = _roleRepository.GetByName("User");
			_accountRoleRepository.Create(new AccountRole
			{
				AccountGuid = account.Guid,
				RoleGuid = roleEmployee.Guid
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

	public string LoginAccount(AccountDtoLogin login)
	{
		var account = _accountRepository.GetEmployeeByEmail(login.Email);
		var employee = _employeeRepository.GetByGuid(account.Guid);
		var overtime = _overtimeRepository.RemainingOvertimeByEmployeeGuid(account.Guid);
		var manager = employee.ManagerGuid != null ? _employeeRepository.GetByGuid(employee.ManagerGuid.Value) : null;
		if (account is null) return "0";

		if (!HashingHandler.Validate(login.Password, account!.Password)) return "-1";

		var employees = _employeeRepository.GetByGuid(account.Guid);
		try
		{
			var claims = new List<Claim>() {
				new Claim("Guid", employee.Guid.ToString()),
				new Claim("Nik", employees.Nik),
				new Claim("FullName", $"{employees.FirstName} {employees.LastName}"),
				new Claim("EmailAddress", login.Email),
				new Claim("Remaining", overtime.Remaining.ToString()),
				new Claim("Manager", manager != null ? $"{manager.FirstName} {manager.LastName}" : "N/A")
			};

			var AccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(account.Guid);
			var getRoleNameByAccountRole = from ar in AccountRole
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
	public AccountDtoForgotPassword ForgotPassword(string email)
	{
		var account = _accountRepository.GetEmployeeByEmail(email);
		if (account is null)
		{
			return null;
		}

		var toDto = new AccountDtoForgotPassword
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

	// Change Password
	public int ChangePassword(AccountDtoChangePassword changePasswordDto)
	{
		var account = _accountRepository.GetEmployeeByEmail(changePasswordDto.Email);
		if (account is null)
			return 0;

		if (account.IsUsed)
			return -1;

		if (account.Otp != changePasswordDto.Otp)
			return -2;

		if (account.ExpiredTime < DateTime.Now)
			return -3;

		var isUpdated = _accountRepository.Update(new Account
		{
			Guid = account.Guid,
			Email = account.Email,
			Password = HashingHandler.Hash(changePasswordDto.NewPassword),
			IsActive = account.IsActive,
			Otp = account.Otp,
			ExpiredTime = account.ExpiredTime,
			IsUsed = true,
			CreatedDate = account.CreatedDate,
			ModifiedDate = DateTime.Now
		});

		return isUpdated ? 1 : -4;
	}

	public IEnumerable<AccountDtoGet> GetAccount()
	{
		var accounts = _accountRepository.GetAll().ToList();
		if (!accounts.Any()) return Enumerable.Empty<AccountDtoGet>();
		List<AccountDtoGet> accountDtos = new();

		foreach (var account in accounts)
		{
			accountDtos.Add((AccountDtoGet)account);
		}

		return accountDtos;
	}

	public AccountDtoGet? GetAccountByGuid(Guid guid)
	{
		var account = _accountRepository.GetByGuid(guid);
		if (account is null) return null;

		return (AccountDtoGet)account;
	}

	public AccountDtoGet? CreateAccount(AccountDtoCreate newAccountDto)
	{
		var createdAccount = _accountRepository.Create(newAccountDto);
		if (createdAccount is null) return null;

		return (AccountDtoGet)createdAccount;
	}

	public int UpdateAccount(AccountDtoUpdate updateAccountDto)
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


