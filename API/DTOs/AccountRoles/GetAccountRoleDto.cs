using API.Models;

namespace API.DTOs.AccountRoles;

public class GetAccountRoleDto
{
    public Guid? AccountGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public static implicit operator AccountRole(GetAccountRoleDto getAccountRoleDto)
    {
        return new()
        {
            AccountGuid = getAccountRoleDto.AccountGuid,
            RoleGuid = getAccountRoleDto.RoleGuid
        };
    }

    public static explicit operator GetAccountRoleDto(AccountRole accountRole)
    {
        return new()
        {
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
