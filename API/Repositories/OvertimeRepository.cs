using API.Contracts;
using API.Data;
using API.DTOs.Overtimes;
using API.Models;

namespace API.Repositories;

public class OvertimeRepository : GeneralRepository<Overtime>, IOvertimeRepository
{
    public OvertimeRepository(OvertimeDbContext context) : base(context) { }
    public string? GetLastOvertimeNumber()
    {
        return _context.Set<Overtime>().ToList().Select(over => over.OvertimeNumber).LastOrDefault();
    }
    public Overtime? CreateOver(Overtime over)
    {
        try
        {
            var remainingOvertime = RemainingOvertimeByEmployeeGuid(over.EmployeeGuid);
            var exitingOvertime = _context.Overtimes.FirstOrDefault(o => o.StartDate.Day == over.StartDate.Day);
            if (remainingOvertime.Remaining > 0)
            {
                var emp = _context.Employees.Where(e => e.Guid == over.EmployeeGuid)
                    .Join(_context.Payslips, e => e.Guid, p => p.EmployeeGuid, (e, p) => new
                    {
                        Employee = e,
                        Payslip = p,
                    }).FirstOrDefault();
                var salaryPerHours = emp.Payslip.Salary * 1 / 173;
                var totalHours = Convert.ToInt32((over.EndDate - over.StartDate).TotalHours);
                var today = over.CreatedDate.DayOfWeek;
                if (today == DayOfWeek.Saturday || today == DayOfWeek.Sunday)
                {
                    if (totalHours > 11)
                    {
                        totalHours = 11;
                    }
                    over.PaidOvertime = TotalPaidWeekend(totalHours, salaryPerHours);
                }
                else
                {
                    if (totalHours > 4)
                    {
                        totalHours = 4;
                    }
                    for (int i = 0; i < totalHours; i++)
                    {
                        if (i < 1)
                        {
                            over.PaidOvertime += Convert.ToInt32(1.5 * salaryPerHours);
                        }
                        else
                        {
                            over.PaidOvertime += 2 * salaryPerHours;
                        }
                    }
                }
                over.Remaining = remainingOvertime.Remaining - totalHours;
                _context.Set<Overtime>().Add(over);
                _context.SaveChanges();
                return over;
            }
            else return null;
        }
        catch (Exception)
        {

            return null;
        }
    }



    public IEnumerable<OvertimeRemainingDto> ListRemainingOvertime()
    {
        var today = DateTime.Today;
        var targetDate = new DateTime(today.Year, 8, 25);
        var endDate = targetDate.AddDays(-30);
        var overRem = (from c in _context.Overtimes
                       join emp in _context.Employees on c.EmployeeGuid equals emp.Guid
                       where (c.Status == Utilities.Enums.StatusLevel.Accepted && c.StartDate >= endDate && c.EndDate <= targetDate)
                       select new
                       {
                           guid = emp.Guid,
                           total = (c.EndDate - c.StartDate).TotalHours
                       }).ToList().GroupBy(a => a.guid).Select(b => new OvertimeRemainingDto()
                       {
                           EmployeeGuid = b.Key,
                           Remaining = Convert.ToInt32(40 - b.Sum(c => c.total)),
                       }).ToList();
        return overRem;
    }

    public IEnumerable<OvertimeRemainingDto> ListRemainingOvertime(Guid guid)
    {
        var today = DateTime.Today;
        var targetDate = new DateTime(today.Year, 8, 25);
        var endDate = targetDate.AddDays(-30);
        var overRem = (from c in _context.Overtimes
                       join emp in _context.Employees on c.EmployeeGuid equals emp.Guid
                       where (c.Status == Utilities.Enums.StatusLevel.Accepted && c.StartDate >= endDate && c.EndDate <= targetDate && emp.Guid == guid)
                       select new OvertimeRemainingDto()
                       {
                           Guid = c.Guid,
                           EmployeeGuid = emp.Guid,
                           EndDate = c.EndDate,
                           StartDate = c.StartDate,
                           FullName = emp.FirstName + " " + emp.LastName,
                           OvertimeNumber = c.OvertimeNumber,
                           Paid = c.PaidOvertime,
                           Remaining = c.Remaining,
                           Remarks = c.Remarks,
                           Status = c.Status,
                           CreatedDate = DateTime.Now
                       }).ToList();
        return overRem;
    }

    public OvertimeRemainingDto RemainingOvertimeByEmployeeGuid(Guid guid)
    {

        var remaining = ListRemainingOvertime();
        var employeeRemaining = remaining.FirstOrDefault(a => a.EmployeeGuid == guid);
        if (employeeRemaining == null)
        {
            var employee = _context.Employees.FirstOrDefault(a => a.Guid == guid);
            var employeeRemain = new OvertimeRemainingDto();
            employeeRemain.EmployeeGuid = employee.Guid;
            employeeRemain.Remaining = 40;
            return employeeRemain;
        }
        return employeeRemaining;

    }

    public int TotalPaidWeekend(int totalHour, double salaryPerHours)
    {
        try
        {
            int paid = 0;
            for (int i = 0; i < totalHour; i++)
            {
                if (i < 8)
                {
                    paid += 2 * (int)salaryPerHours;
                }
                else if (i == 8)
                {
                    paid += 3 * (int)salaryPerHours;
                }
                else
                {
                    paid += 4 * (int)salaryPerHours;
                }
            }
            return paid;

        }
        catch
        {
            return 0;
        }
    }
}
