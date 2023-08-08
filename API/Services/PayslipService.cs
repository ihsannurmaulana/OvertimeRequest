using API.Contracts;
using API.DTOs.Payslips;
using API.Models;

namespace API.Services;

public class PayslipService
{
    private readonly IPayslipRepository _payslipRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOvertimeRepository _overtimeRepository;

    public PayslipService(IPayslipRepository payslipRepository, IEmployeeRepository employeeRepository, IOvertimeRepository overtimeRepository)
    {
        _payslipRepository = payslipRepository;
        _employeeRepository = employeeRepository;
        _overtimeRepository = overtimeRepository;
    }

    public IEnumerable<PayslipDtoGet> GetPayslip()
    {
        var master = (from e in _employeeRepository.GetAll()
                      join p in _payslipRepository.GetAll() on e.Guid equals p.EmployeeGuid
                      where e.Guid == p.EmployeeGuid
                      select new PayslipDtoGet
                      {
                          Guid = p.Guid,
                          Date = DateTime.Now,
                          EmployeeGuid = e.Guid,
                          Allowace = p.Allowance,
                          Salary = p.Salary,
                          EmployeeName = e.FirstName + " " + e.LastName
                      }).ToList();
        if (!master.Any())
        {
            return Enumerable.Empty<PayslipDtoGet>();
        }
        return master;
        //var employee = _employeeRepository.GetAll();
        //var payslip = _payslipRepository.GetAll().ToList();
        //if (!payslip.Any())
        //{
        //    return Enumerable.Empty<PayslipDtoGet>();
        //}
        //List<PayslipDtoGet> payslipDtos = new();

        //foreach (var pays in payslip)
        //{
        //    payslipDtos.Add((PayslipDtoGet)pays);
        //}

        //return payslipDtos;
    }

    public PayslipDtoGet? GetPayslipDtoByGuid(Guid guid)
    {
        var payslip = _payslipRepository.GetByGuid(guid);
        if (payslip == null)
        {
            return null;
        }

        return (PayslipDtoGet)payslip;
    }

    public PayslipDtoGet? CreatePayslip(PayslipDtoCreate newPayslip)
    {
        Payslip payslips = newPayslip;
        var payslip = _payslipRepository.Create(payslips);
        if (payslip == null) return null;

        return (PayslipDtoGet)payslip;
    }

    public int UpdatePayslips(PayslipDtoUpdate updatePayslip)
    {
        var payslipps = _payslipRepository.GetByGuid(updatePayslip.Guid);
        if (payslipps == null)
        {
            return -1;
        }
        var isUpdate = _payslipRepository.Update(updatePayslip);
        return !isUpdate ? 0 : 1;
    }

    public int DeletePayslip(Guid guid)
    {
        var payslip = _payslipRepository.GetByGuid(guid);
        if (payslip == null) { return -1; }

        var isDelete = _payslipRepository.Delete(payslip);
        return !isDelete ? 0 : 1;
    }

    public IEnumerable<PayslipDtoGetMaster> GetAllOver()
    {
        var payslip = (from p in _payslipRepository.GetAll()
                       join o in _overtimeRepository.GetAll() on p.EmployeeGuid equals o.EmployeeGuid
                       select new
                       {
                           guid = o.EmployeeGuid,
                           totalOver = o.PaidOvertime,
                       }).ToList().GroupBy(a => a.guid).Select(b => new PayslipDtoGetMaster()
                       {
                           EmployeeGuid = b.Key,
                           PaidOvertime = b.Sum(c => c.totalOver)
                       }).ToList();
        return payslip;
    }

    public IEnumerable<PayslipDtoGetMaster> GetPayslipMaster()
    {
        var payslip = (from p in _payslipRepository.GetAll()
                       join o in _overtimeRepository.GetAll() on p.EmployeeGuid equals o.EmployeeGuid into joined
                       from o in joined.DefaultIfEmpty()
                       where o == null // This line will filter out the rows where there is no matching EmployeeGuid in the overtime table.
                       select new
                       {
                           guid = p.EmployeeGuid,
                           totalOver = 0 // Set the PaidOvertime to 0 since there is no matching overtime record.
                       }).ToList().GroupBy(a => a.guid).Select(b => new PayslipDtoGetMaster()
                       {
                           EmployeeGuid = b.Key,
                           PaidOvertime = b.Sum(c => c.totalOver)
                       }).ToList();

        return payslip;
    }

    public IEnumerable<PayslipDtoGetMaster> GetAllOver(Guid guid)
    {
        var payslip = (from p in _payslipRepository.GetAll()
                       join o in _overtimeRepository.GetAll() on p.EmployeeGuid equals o.EmployeeGuid
                       where p.Guid == guid
                       select new
                       {

                           guid = o.EmployeeGuid,
                           totalOver = o.PaidOvertime,
                       }).ToList().GroupBy(a => a.guid).Select(b => new PayslipDtoGetMaster()
                       {
                           EmployeeGuid = b.Key,
                           PaidOvertime = b.Sum(c => c.totalOver)
                       }).ToList();
        return payslip;
    }
    public IEnumerable<PayslipDtoGetMaster> GetAllOverEmpGuid(Guid guid)
    {
        var payslip = (from p in _payslipRepository.GetAll()
                       join o in _overtimeRepository.GetAll() on p.EmployeeGuid equals o.EmployeeGuid
                       where p.EmployeeGuid == guid
                       select new
                       {
                           guid = o.EmployeeGuid,
                           totalOver = o.PaidOvertime,
                       }).ToList().GroupBy(a => a.guid).Select(b => new PayslipDtoGetMaster()
                       {
                           EmployeeGuid = b.Key,
                           PaidOvertime = b.Sum(c => c.totalOver)
                       }).ToList();
        return payslip;
    }

    public IEnumerable<PayslipDtoGetMaster> GetAllMasterOver()
    {
        var payslip = GetAllOver();
        var payslips = new List<PayslipDtoGetMaster>();
        payslips = payslip.ToList();

        var noOvertime = GetPayslipMaster();
        var noOverTimes = new List<PayslipDtoGetMaster>();
        noOverTimes = noOvertime.ToList();

        payslips.AddRange(noOverTimes);

        var employee = (from p in _payslipRepository.GetAll()
                        join e in _employeeRepository.GetAll() on p.EmployeeGuid equals e.Guid
                        join pay in payslips on p.EmployeeGuid equals pay.EmployeeGuid
                        select new PayslipDtoGetMaster
                        {
                            Guid = p.Guid,
                            Salary = p.Salary,
                            Allowance = p.Allowance,
                            Date = DateTime.Now,
                            PaidOvertime = pay.PaidOvertime,
                            TotalSalary = p.Salary + pay.TotalSalary - p.Allowance,
                            EmployeeGuid = pay.EmployeeGuid,
                            FullName = e.FirstName + " " + e.LastName,
                        }).ToList();
        return employee;
    }

    public IEnumerable<PayslipDtoGetMaster> GetAllMasterOverbyGuid(Guid guid)
    {
        var payslip = GetAllOver(guid);
        var employee = (from p in _payslipRepository.GetAll()
                        join pay in payslip on p.EmployeeGuid equals pay.EmployeeGuid
                        select new PayslipDtoGetMaster
                        {
                            Guid = p.Guid,
                            Salary = p.Salary,
                            Allowance = p.Allowance,
                            Date = p.Date,
                            PaidOvertime = pay.PaidOvertime,
                            TotalSalary = p.Salary + pay.TotalSalary - p.Allowance,
                            EmployeeGuid = pay.EmployeeGuid,
                        }).ToList();
        return employee;
    }

    public IEnumerable<PayslipDtoGetMaster> GetAllMasterOverbyEmpGuid(Guid guid)
    {
        var payslip = GetAllOverEmpGuid(guid);
        var employee = (from p in _payslipRepository.GetAll()
                        join pay in payslip on p.EmployeeGuid equals pay.EmployeeGuid
                        select new PayslipDtoGetMaster
                        {
                            Guid = p.Guid,
                            Salary = p.Salary,
                            Allowance = p.Allowance,
                            Date = p.Date,
                            PaidOvertime = pay.PaidOvertime,
                            TotalSalary = p.Salary + pay.TotalSalary - p.Allowance,
                            EmployeeGuid = pay.EmployeeGuid,
                        }).ToList();
        return employee;
    }

    public double GetTotalSalaryExpense()
    {
        var payslips = _payslipRepository.GetAll();

        // Calculate total salary expense by summing up all salaries
        double totalExpense = payslips.Sum(p => p.Salary);

        return totalExpense;
    }
    public double GetTotalOvertime(Guid guid)
    {
        var payslips = GetAllMasterOverbyEmpGuid(guid);
        double totalOvertime = payslips.Sum(p => p.PaidOvertime);
        return totalOvertime;
    }
}
