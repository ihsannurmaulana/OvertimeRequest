using API.Models;

namespace API.DTOs.Roles;

public class UpdateRoleDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(UpdateRoleDto updateRoleDto)
    {
        return new()
        {
            Guid = updateRoleDto.Guid,
            Name = updateRoleDto.Name
        };
    }

    public static explicit operator UpdateRoleDto(Role role)
    {
        return new()
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }
}
