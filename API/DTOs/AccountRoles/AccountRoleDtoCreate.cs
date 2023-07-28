using API.Models;

namespace API.DTOs.AccountRoles;

public class AccountRoleDtoCreate
{
    public Guid AccountGuid { get; set; }

    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoCreate newAccountRoleDto)
    {
        return new()
        {
            Guid = new Guid(),
            AccountGuid = newAccountRoleDto.AccountGuid,
            RoleGuid = newAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator AccountRoleDtoCreate(AccountRole accountRole)
    {
        return new()
        {
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
