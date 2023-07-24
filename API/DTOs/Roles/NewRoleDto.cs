using API.Models;

namespace API.DTOs.Roles;

public class NewRoleDto
{
    public string Name { get; set; }

    public static implicit operator Role(NewRoleDto newRoleDto)
    {
        return new()
        {
            Name = newRoleDto.Name
        };
    }

    public static explicit operator NewRoleDto(Role role)
    {
        return new()
        {
            Name = role.Name
        };
    }
}
