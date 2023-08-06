using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Payslips;

namespace ClientOvertime.Contracts;

public interface IPayslipRepository : IGeneralRepository<PayslipVMGet, Guid>
{
    Task<ResponseHandler<PayslipVMGet>> GetDetail(Guid guid);
}
