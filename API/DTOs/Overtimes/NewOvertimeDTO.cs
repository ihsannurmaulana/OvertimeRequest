using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class NewOvertimeDTO
{
    public int OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid? PayslipGuid { get; set; }


    public static implicit operator Overtime(NewOvertimeDTO newOver)
    {
        return new()
        {
            Guid = new Guid(),
            StartDate = newOver.StartDate,
            EndDate = newOver.EndDate,
            Remarks = newOver.Remarks,
            Status = newOver.Status,
            EmployeeGuid = newOver.EmployeeGuid,
            PayslipGuid = newOver.PayslipGuid,
        };
    }

    public static explicit operator NewOvertimeDTO(Overtime overtime)
    {
        return new()
        {
            StartDate = overtime.StartDate,
            EndDate = overtime.EndDate,
            Remarks = overtime.Remarks,
            Status = overtime.Status,
            EmployeeGuid = overtime.EmployeeGuid,
            PayslipGuid = overtime.PayslipGuid
        };

    }
}
