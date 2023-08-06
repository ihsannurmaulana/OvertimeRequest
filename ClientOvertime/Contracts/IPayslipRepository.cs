using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Payslips;

namespace ClientOvertime.Contracts;

public interface IPayslipRepository : IGeneralRepository<PayslipVMGet, Guid>
{
    Task<ResponseHandler<PayslipVMGet>> GetDetail(Guid guid);
     public Task<ResponseHandler<PayslipsViewModelUpdate>> PutSalary(Guid id, PayslipsViewModelUpdate entity);
     public Task<ResponseHandler<double>> GetStatistic();
}
