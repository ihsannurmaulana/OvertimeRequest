using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class PayslipRepository : GeneralRepository<Payslip>, IPayslipRepository
{
    public PayslipRepository(OvertimeDbContext context) : base(context) { }
}
