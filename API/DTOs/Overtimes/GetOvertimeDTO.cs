using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class GetOvertimeDTO
{
    public Guid Guid { get; set; }
    public string OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }

    public static implicit operator Overtime(GetOvertimeDTO getOvertime)
    {
        return new()
        {
            Guid = getOvertime.Guid,
            OvertimeNumber = getOvertime.OvertimeNumber,
            StartDate = getOvertime.StartDate,
            EndDate = getOvertime.EndDate,
            Remarks = getOvertime.Remarks,
            Status = getOvertime.Status,
            EmployeeGuid = getOvertime.EmployeeGuid,
        };
    }

    public static explicit operator GetOvertimeDTO(Overtime overtime)
    {
        return new()
        {
            Guid = overtime.Guid,
            OvertimeNumber = overtime.OvertimeNumber,
            StartDate = overtime.StartDate,
            EndDate = overtime.EndDate,
            Remarks = overtime.Remarks,
            Status = overtime.Status,
            EmployeeGuid = overtime.EmployeeGuid,
        };
    }

}
