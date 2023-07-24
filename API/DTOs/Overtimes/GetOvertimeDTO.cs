using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class GetOvertimeDTO
{
    public Guid Guid { get; set; }
    public int OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid PayslipGuid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
