using API.Models;

namespace API.DTOs.Payslips;

public class NewPayslipDto
{
    public DateTime Date { get; set; }
    public double Salary { get; set; }
    public double Allowace { get; set; }
    public Guid? EmployeeGuid { get; set; }

    public static implicit operator Payslip(NewPayslipDto newPayslips)
    {
        return new()
        {
            Guid = new Guid(),
            Date = newPayslips.Date,
            Salary = newPayslips.Salary,
            Allowance = newPayslips.Allowace,
            EmployeeGuid = newPayslips.EmployeeGuid,
        };
    }

    public static explicit operator NewPayslipDto(Payslip payslip)
    {
        return new()
        {
            Date = payslip.Date,
            Allowace = payslip.Allowance,
            Salary = payslip.Salary,
            EmployeeGuid = payslip.EmployeeGuid
        };
    }

}
