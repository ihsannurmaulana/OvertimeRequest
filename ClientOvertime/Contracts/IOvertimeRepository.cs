using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Overtimes;

namespace ClientOvertime.Contracts;

public interface IOvertimeRepository : IGeneralRepository<OvertimeVMRequest, Guid>
{
    //Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetHistory();
    Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetOverManager(Guid guid);
    //Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetOvertimeDetail(Guid guid);
}
