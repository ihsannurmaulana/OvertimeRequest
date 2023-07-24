using API.Models;

namespace API.DTOs.Roles;

public class GetRoleDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(GetRoleDto updateRoleDto)
    {
        return new()
        {
            Guid = updateRoleDto.Guid,
            Name = updateRoleDto.Name
        };
    }

    public static explicit operator GetRoleDto(Role role)
    {
        return new()
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }
}
