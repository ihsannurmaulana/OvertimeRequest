using API.Models;

namespace API.DTOs.Accounts;

public class UpdateAccountDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int? Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }

    public static implicit operator Account(UpdateAccountDto updateAccountDto)
    {
        return new()
        {
            Guid = updateAccountDto.Guid,
            Email = updateAccountDto.Email,
            Password = updateAccountDto.Password,
            IsActive = updateAccountDto.IsActive,
            Otp = updateAccountDto.Otp,
            IsUsed = updateAccountDto.IsUsed,
            ExpiredTime = updateAccountDto.ExpiredTime,
            ModifiedDate = DateTime.UtcNow
        };
    }

    public static explicit operator UpdateAccountDto(Account account)
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
