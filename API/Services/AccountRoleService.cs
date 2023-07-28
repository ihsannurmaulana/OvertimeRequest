using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services;

public class AccountRoleService
{
    private readonly IAccountRoleRepository _accountRoleRepository;

    public AccountRoleService(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    public IEnumerable<AccountRoleDtoGet> GetAccountRole()
    {
        var accountRoles = _accountRoleRepository.GetAll().ToList();
        if (!accountRoles.Any()) return Enumerable.Empty<AccountRoleDtoGet>();
        List<AccountRoleDtoGet> accountRoleDtos = new();

        foreach (var accountRole in accountRoles)
        {
            accountRoleDtos.Add((AccountRoleDtoGet)accountRole);
        }

        return accountRoleDtos;
    }

    public AccountRoleDtoGet? GetAccountRoleByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null) return null;

        return (AccountRoleDtoGet) accountRole;
    }

    public AccountRoleDtoGet? CreateAccountRole(AccountRoleDtoCreate newAccountRoleDto)
    {
        var createdAccountRole = _accountRoleRepository.Create(newAccountRoleDto);
        if (createdAccountRole is null) return null;

        return (AccountRoleDtoGet)createdAccountRole;
    }

    public int UpdateAccountRole(AccountRoleDtoUpdate updateAccountRoleDto)
    {
        var getAccountRole = _accountRoleRepository.GetByGuid(updateAccountRoleDto.Guid);

        if (getAccountRole is null) return -1;

        var isUpdate = _accountRoleRepository.Update(updateAccountRoleDto);
        return !isUpdate ? 0 :
            1;               
    }

    public int DeleteAccountRole(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);

        if (accountRole is null) return -1;

        var isDelete = _accountRoleRepository.Delete(accountRole);
        return !isDelete ? 0 :
            1;                 
    }
}