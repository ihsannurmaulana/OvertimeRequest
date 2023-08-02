using API.Models;

namespace API.DTOs.Payslips;

public class PayslipDtoCreate
{
    public double Salary { get; set; }
    //public double Allowace { get; set; }
    public Guid EmployeeGuid { get; set; }

    public static implicit operator Payslip(PayslipDtoCreate newPayslips)
    {
        return new()
        {
            Guid = new Guid(),
            Salary = newPayslips.Salary,
            //Allowance = newPayslips.Allowace,
            EmployeeGuid = newPayslips.EmployeeGuid,
        };
    }

    public static explicit operator PayslipDtoCreate(Payslip payslip)
    {
        return new()
        {
            //Allowace = payslip.Allowance,
            Salary = payslip.Salary,
            EmployeeGuid = payslip.EmployeeGuid
        };
    }

}
