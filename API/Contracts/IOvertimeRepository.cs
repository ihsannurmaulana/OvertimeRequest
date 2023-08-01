using API.DTOs.Overtimes;
using API.Models;

namespace API.Contracts;

public interface IOvertimeRepository : IGeneralRepository<Overtime>
{
	public string? GetLastOvertimeNumber();
	public Overtime CreateOver(Overtime overtime);
	public int TotalPaidWeekend(int totalHour, double salaryPerHours);
	public IEnumerable<OvertimeRemainingDto> ListRemainingOvertime();
	public IEnumerable<OvertimeRemainingDto> ListRemainingOvertime(Guid id);
	public OvertimeRemainingDto RemainingOvertimeByEmployeeGuid(Guid id);

}
