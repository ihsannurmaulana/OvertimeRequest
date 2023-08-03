using API.Utilities.Enums;

namespace ClientOvertime.ViewModels.Overtimes;

public class OvertimeVMRequest
{
    public Guid Guid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }
    public string OvertimeNumber { get; set; }
    public string Paid { get; set; }
    public string Remaining { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedDate { get; set; }
}
