using API.Models;

namespace API.DTOs.Accounts;

public class UpdateAccountDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    public static implicit operator Account(UpdateAccountDto updateAccountDto)
    {
        return new()
        {
            Guid = updateAccountDto.Guid,
            Email = updateAccountDto.Email,
            Password = updateAccountDto.Password,
            IsUsed = updateAccountDto.IsUsed,
            Otp = updateAccountDto.Otp,
            ExpiredTime = updateAccountDto.ExpiredTime,
            IsActive = updateAccountDto.IsActive,
        };
    }

    public static explicit operator UpdateAccountDto(Account account)
    {
        return new()
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password,
            IsUsed = account.IsUsed,
            Otp = account.Otp,
            ExpiredTime = account.ExpiredTime,
            IsActive = account.IsActive,
        };
    }
}
