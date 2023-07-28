using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class OvertimeDtoCreate
{
    // public string OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }


    public static implicit operator Overtime(OvertimeDtoCreate newOver)
    {
        return new()
        {
            Guid = new Guid(),
            StartDate = newOver.StartDate,
            EndDate = newOver.EndDate,
            Remarks = newOver.Remarks,
            Status = newOver.Status,
            EmployeeGuid = newOver.EmployeeGuid
        };
    }

    public static explicit operator OvertimeDtoCreate(Overtime overtime)
    {
        return new()
        {
            StartDate = overtime.StartDate,
            EndDate = overtime.EndDate,
            Remarks = overtime.Remarks,
            Status = overtime.Status,
            EmployeeGuid = overtime.EmployeeGuid
        };

    }
}
