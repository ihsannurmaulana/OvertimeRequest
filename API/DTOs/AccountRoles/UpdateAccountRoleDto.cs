using API.Models;

namespace API.DTOs.AccountRoles;

public class UpdateAccountRoleDto
{
    public Guid Guid { get; set; }

    public Guid? AccountGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public static implicit operator AccountRole(UpdateAccountRoleDto updateAccountRoleDto)
    {
        return new()
        {
            Guid = updateAccountRoleDto.Guid,
            AccountGuid = updateAccountRoleDto.AccountGuid,
            RoleGuid = updateAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator UpdateAccountRoleDto(AccountRole accountRole)
    {
        return new()
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
