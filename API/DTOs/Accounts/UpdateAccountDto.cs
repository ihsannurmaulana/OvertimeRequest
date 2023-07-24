using API.Models;

namespace API.DTOs.Accounts;

public class UpdateAccountDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public static implicit operator Account(UpdateAccountDto updateAccountDto)
    {
        return new()
        {
            Guid = updateAccountDto.Guid,
            Email = updateAccountDto.Email,
            Password = updateAccountDto.Password
        };
    }

    public static explicit operator UpdateAccountDto(Account account)
    {
        return new()
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password
        };
    }
}
