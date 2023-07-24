using API.Models;

namespace API.DTOs.AccountRoles;

public class NewAccountRoleDto
{
    public Guid Guid { get; set; }

    public Guid? AccountGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public static implicit operator AccountRole(NewAccountRoleDto newAccountRoleDto)
    {
        return new()
        {
            Guid = newAccountRoleDto.Guid,
            AccountGuid = newAccountRoleDto.AccountGuid,
            RoleGuid = newAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator NewAccountRoleDto(AccountRole accountRole)
    {
        return new()
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
