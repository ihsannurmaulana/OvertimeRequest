using ClientOvertime.ViewModels.Payslips;

namespace ClientOvertime.Contracts;

public interface IPayslipRepository : IGeneralRepository<PayslipVMGet, Guid>
{
}
