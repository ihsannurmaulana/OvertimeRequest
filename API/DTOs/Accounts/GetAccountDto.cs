using API.Models;

namespace API.DTOs.Accounts;

public class GetAccountDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public static implicit operator Account(GetAccountDto getAccountDto)
    {
        return new()
        {
            Guid = getAccountDto.Guid,
            Email = getAccountDto.Email,
            Password = getAccountDto.Password
        };
    }

    public static explicit operator GetAccountDto(Account account)
    {
        return new()
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password
        };
    }
}
