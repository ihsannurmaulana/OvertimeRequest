namespace API.DTOs.Accounts;

public class ForgotPasswordDto
{
    public string Email { get; set; }
    public int Otp { get; set; }
    public DateTime ExpiredTime { get; set; }
}
