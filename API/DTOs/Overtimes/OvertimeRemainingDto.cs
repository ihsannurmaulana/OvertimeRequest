using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class OvertimeRemainingDto
{
	public Guid Guid { get; set; }
	public string OvertimeNumber { get; set; }
	public DateTime StartDate { get; set; }
	public string FullName { get; set; }
	public DateTime EndDate { get; set; }
	public double Paid { get; set; }
	public string? Remarks { get; set; }
	public int Remaining { get; set; }
	public StatusLevel Status { get; set; }
	public Guid EmployeeGuid { get; set; }

	public static implicit operator Overtime(OvertimeRemainingDto getOvertime)
	{
		return new()
		{
			Guid = getOvertime.Guid,
			OvertimeNumber = getOvertime.OvertimeNumber,
			StartDate = DateTime.Today,
			EndDate = getOvertime.EndDate,
			PaidOvertime = getOvertime.Paid,
			Remaining = getOvertime.Remaining,
			Remarks = getOvertime.Remarks,
			Status = getOvertime.Status,
			EmployeeGuid = getOvertime.EmployeeGuid,
		};
	}

	public static explicit operator OvertimeRemainingDto(Overtime overtime)
	{
		return new()
		{
			Guid = overtime.Guid,
			OvertimeNumber = overtime.OvertimeNumber,
			StartDate = overtime.StartDate,
			Paid = overtime.PaidOvertime,
			Remaining = overtime.Remaining,
			EndDate = overtime.EndDate,
			Remarks = overtime.Remarks,
			Status = overtime.Status,
			EmployeeGuid = overtime.EmployeeGuid,
		};
	}
}
