using API.Models;

namespace API.DTOs.Accounts;

public class NewAccountDto
{
    public Guid Guid { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int? Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }


    public static implicit operator Account(NewAccountDto newAccountDto)
    {
        return new()
        {
            Guid = newAccountDto.Guid,
            Email = newAccountDto.Email,
            Password = newAccountDto.Password,
            IsActive = newAccountDto.IsActive,
            Otp = newAccountDto.Otp,
            IsUsed = newAccountDto.IsUsed,
            ExpiredTime = newAccountDto.ExpiredTime,
            CreatedDate = DateTime.UtcNow
        };
    }

    public static explicit operator NewAccountDto(Account account)
    {
        return new()
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password,
            IsActive = account.IsActive,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}