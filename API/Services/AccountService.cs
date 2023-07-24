using API.Contracts;
using API.DTOs.Accounts;
using API.Models;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
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

        return (GetAccountDto) account;
    }

    public GetAccountDto? CreateAccount(NewAccountDto newAccountDto)
    {
        Account account = newAccountDto;
        var createdAccount = _accountRepository.Create(account);
        if (createdAccount is null) return null;

        return (GetAccountDto) createdAccount;
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
