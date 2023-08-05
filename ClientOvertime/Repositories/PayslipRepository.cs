using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Payslips;

namespace ClientOvertime.Repositories;

public class PayslipRepository : GeneralRepository<PayslipVMGet, Guid>, IPayslipRepository
{
    public PayslipRepository(string request = "payslips/dummy") : base(request)
    {
    }
}
