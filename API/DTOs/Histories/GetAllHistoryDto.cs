using API.Utilities.Enums;

namespace API.DTOs.Histories;

public class GetAllHistoryDto
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FullName { get; set; }
    public string OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? ApproveBy { get; set; }
    public DateTime Submited { get; set; }
    public Guid? ManagerGuid { get; set; }
    public StatusLevel Status { get; set; }
}
