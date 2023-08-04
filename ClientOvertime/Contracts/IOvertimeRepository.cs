using API.Utilities.Handlers;
using ClientOvertime.Controllers;
using ClientOvertime.ViewModels.Overtimes;

namespace ClientOvertime.Contracts;

public interface IOvertimeRepository : IGeneralRepository<OvertimeVMRequest, Guid>
{
    //Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetHistory();
    Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetOverManager(Guid guid);
    //Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetOvertimeDetail(Guid guid);
    public Task<ResponseHandler<ApprovalManager>> PutApproval(ApprovalManager entity);
}
