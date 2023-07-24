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

    public IEnumerable<GetAccountRoleDto> GetAccountRole()
    {
        var accountRoles = _accountRoleRepository.GetAll().ToList();
        if (!accountRoles.Any()) return Enumerable.Empty<GetAccountRoleDto>();
        List<GetAccountRoleDto> accountRoleDtos = new();

        foreach (var accountRole in accountRoles)
        {
            accountRoleDtos.Add((GetAccountRoleDto)accountRole);
        }

        return accountRoleDtos;
    }

    public GetAccountRoleDto? GetAccountRoleByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null) return null;

        return (GetAccountRoleDto) accountRole;
    }

    public GetAccountRoleDto? CreateAccountRole(NewAccountRoleDto newAccountRoleDto)
    {
        var createdAccountRole = _accountRoleRepository.Create(newAccountRoleDto);
        if (createdAccountRole is null) return null;

        return (GetAccountRoleDto)createdAccountRole;
    }

    public int UpdateAccountRole(UpdateAccountRoleDto updateAccountRoleDto)
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