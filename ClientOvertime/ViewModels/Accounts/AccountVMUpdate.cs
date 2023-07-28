namespace ClientOvertime.ViewModels.Accounts;

public class AccountVMUpdate
{
    public Guid Guid { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int? Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
}
