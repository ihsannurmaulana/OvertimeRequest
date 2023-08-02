using API.Models;

namespace API.DTOs.Payslips;

public class PayslipDtoGet
{
    public Guid Guid { get; set; }
    public DateTime Date { get; set; }
    public double Salary { get; set; }
    public double Allowace { get; set; }
    public Guid? EmployeeGuid { get; set; }
    public string EmployeeName { get; set; }


    public static implicit operator Payslip(PayslipDtoGet payslipDto)
    {
        return new()
        {
            Guid = payslipDto.Guid,
            Date = DateTime.Now,
            Salary = payslipDto.Salary,
            Allowance = payslipDto.Salary * 3 / 100,
            EmployeeGuid = payslipDto.EmployeeGuid,
        };
    }

    public static explicit operator PayslipDtoGet(Payslip payslip)
    {
        return new()
        {
            Guid = payslip.Guid,
            Date = DateTime.Now,
            Salary = payslip.Salary,
            Allowace = payslip.Salary * 3 / 100,
            EmployeeGuid = payslip.EmployeeGuid,
        };
    }
}
