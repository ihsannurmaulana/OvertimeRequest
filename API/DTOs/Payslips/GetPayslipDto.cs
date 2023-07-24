using API.Models;

namespace API.DTOs.Payslips;

public class GetPayslipDto
{
    public Guid Guid { get; set; }
    public DateTime Date { get; set; }
    public double Salary { get; set; }
    public double Allowace { get; set; }
    public Guid? EmployeeGuid { get; set; }


    public static implicit operator Payslip(GetPayslipDto payslipDto)
    {
        return new()
        {
            Guid = payslipDto.Guid,
            Date = payslipDto.Date,
            Salary = payslipDto.Salary,
            Allowance = payslipDto.Allowace,
            EmployeeGuid = payslipDto.EmployeeGuid,
        };
    }

    public static explicit operator GetPayslipDto(Payslip payslip)
    {
        return new()
        {
            Guid = payslip.Guid,
            Date = payslip.Date,
            Salary = payslip.Salary,
            Allowace = payslip.Allowance,
            EmployeeGuid = payslip.EmployeeGuid,
        };
    }
}
