using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Overtimes;

namespace ClientOvertime.Repositories;

public class OvertimeRepository : GeneralRepository<OvertimeVMRequest, Guid>, IOvertimeRepository
{
	public OvertimeRepository(string request = "overtimes/dummy") : base(request)
	{
	}
}
