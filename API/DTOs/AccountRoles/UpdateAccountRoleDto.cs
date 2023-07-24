using API.Models;

namespace API.DTOs.AccountRoles;

public class UpdateAccountRoleDto
{
    public Guid? AccountGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public static implicit operator AccountRole(UpdateAccountRoleDto updateAccountRoleDto)
    {
        return new()
        {
            AccountGuid = updateAccountRoleDto.AccountGuid,
            RoleGuid = updateAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator UpdateAccountRoleDto(AccountRole accountRole)
    {
        return new()
        {
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
