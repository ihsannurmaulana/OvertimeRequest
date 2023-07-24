using API.Models;

namespace API.DTOs.AccountRoles;

public class NewAccountRoleDto
{
    public Guid? AccountGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public static implicit operator AccountRole(NewAccountRoleDto newAccountRoleDto)
    {
        return new()
        {
            Guid = new Guid(),
            AccountGuid = newAccountRoleDto.AccountGuid,
            RoleGuid = newAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator NewAccountRoleDto(AccountRole accountRole)
    {
        return new()
        {
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
