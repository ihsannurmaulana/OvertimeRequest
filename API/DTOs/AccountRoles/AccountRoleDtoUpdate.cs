using API.Models;

namespace API.DTOs.AccountRoles;

public class AccountRoleDtoUpdate
{
    public Guid Guid { get; set; }

    public Guid AccountGuid { get; set; }

    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoUpdate updateAccountRoleDto)
    {
        return new()
        {
            Guid = updateAccountRoleDto.Guid,
            AccountGuid = updateAccountRoleDto.AccountGuid,
            RoleGuid = updateAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator AccountRoleDtoUpdate(AccountRole accountRole)
    {
        return new()
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
