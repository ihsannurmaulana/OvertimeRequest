using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Overtimes;

namespace ClientOvertime.Contracts;

public interface IOvertimeRepository : IGeneralRepository<OvertimeVMRequest, Guid>
{
    Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetHistory();
}
