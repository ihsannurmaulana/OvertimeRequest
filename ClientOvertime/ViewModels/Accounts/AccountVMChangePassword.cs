namespace ClientOvertime.ViewModels.Accounts;

public class AccountVMChangePassword
{
    public string Email { get; set; }
    public int Otp { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
