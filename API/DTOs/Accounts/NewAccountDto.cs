using API.Models;

namespace API.DTOs.Accounts;

public class NewAccountDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public static implicit operator Account(NewAccountDto newAccountDto)
    {
        return new()
        {
            Guid = newAccountDto.Guid,
            Email = newAccountDto.Email,
            Password = newAccountDto.Password
        };
    }

    public static explicit operator NewAccountDto(Account account)
    {
        return new()
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password
        };
    }
}