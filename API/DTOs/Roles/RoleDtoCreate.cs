using API.Models;

namespace API.DTOs.Roles;

public class RoleDtoCreate
{
    public string Name { get; set; }

    public static implicit operator Role(RoleDtoCreate newRoleDto)
    {
        return new()
        {
            Name = newRoleDto.Name
        };
    }

    public static explicit operator RoleDtoCreate(Role role)
    {
        return new()
        {
            Name = role.Name
        };
    }
}
