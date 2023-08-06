using API.Models;

namespace API.DTOs.Payslips;

//public class UpdatePayslip : PayslipDtoGet
//{
//}

public class PayslipDtoUpdate
{
    public Guid Guid { get; set; }
    public double Salary { get; set; }
    public Guid EmployeeGuid { get; set; }


    public static implicit operator Payslip(PayslipDtoUpdate payslipDto)
    {
        return new()
        {
            Guid = payslipDto.Guid,
            Salary = payslipDto.Salary,
            Allowance = payslipDto.Salary * 3 / 100,
            EmployeeGuid = payslipDto.EmployeeGuid,
        };
    }

    public static explicit operator PayslipDtoUpdate(Payslip payslip)
    {
        return new()
        {
            Guid = payslip.Guid,
            Salary = payslip.Salary,
            EmployeeGuid = payslip.EmployeeGuid,
        };
    }
}