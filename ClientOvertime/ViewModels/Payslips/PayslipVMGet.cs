namespace ClientOvertime.ViewModels.Payslips;

public class PayslipVMGet
{
    public Guid Guid { get; set; }
    public DateTime Date { get; set; }
    public int Salary { get; set; }
    public double Allowace { get; set; }
    public Guid? EmployeeGuid { get; set; }
    public string EmployeeName { get; set; }
}
