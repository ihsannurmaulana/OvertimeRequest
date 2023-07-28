using API.Models;

namespace API.DTOs.AccountRoles;

public class AccountRoleDtoGet
{
    public Guid Guid { get; set; }

    public Guid AccountGuid { get; set; }

    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoGet getAccountRoleDto)
    {
        return new()
        {
            Guid = getAccountRoleDto.Guid,
            AccountGuid = getAccountRoleDto.AccountGuid,
            RoleGuid = getAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator AccountRoleDtoGet(AccountRole accountRole)
    {
        return new()
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
