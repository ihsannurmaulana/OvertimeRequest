﻿using API.Models;

namespace API.DTOs.Accounts;

public class AccountDtoGet
{
    public Guid Guid { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int? Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    //public DateTime CreatedDate { get; set; }
    //public DateTime ModifiedDate { get; set; }

    public static implicit operator Account(AccountDtoGet getAccountDto)
    {
        return new()
        {
            Guid = getAccountDto.Guid,
            Email = getAccountDto.Email,
            Password = getAccountDto.Password,
            IsActive = getAccountDto.IsActive,
            Otp = getAccountDto.Otp,
            IsUsed = getAccountDto.IsUsed,
            ExpiredTime = getAccountDto.ExpiredTime
            //CreatedDate = DateTime.MinValue,
            //ModifiedDate = DateTime.MinValue,
        };
    }

    public static explicit operator AccountDtoGet(Account account)
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
