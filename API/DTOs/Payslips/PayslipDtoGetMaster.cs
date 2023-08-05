using API.Models;

namespace API.DTOs.Payslips
{
    public class PayslipDtoGetMaster
    {
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }
        public double Salary { get; set; }
        public double Allowance { get; set; }
        public double PaidOvertime { get; set; }
        public double TotalSalary { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string FullName { get; set; }

        public static implicit operator Payslip(PayslipDtoGetMaster payslipDto)
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

        public static explicit operator PayslipDtoGetMaster(Payslip payslip)
        {
            return new()
            {
                Guid = payslip.Guid,
                Date = DateTime.Now,
                Salary = payslip.Salary,
                Allowance = payslip.Salary * 3 / 100,
                EmployeeGuid = payslip.EmployeeGuid,
            };
        }


    }
}
