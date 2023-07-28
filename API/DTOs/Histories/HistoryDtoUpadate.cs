using API.Models;

namespace API.DTOs.Histories;

public class HistoryDtoUpadate
{
    public Guid Guid { get; set; }

    public Guid OvertimeGuid { get; set; }

    public static implicit operator History(HistoryDtoUpadate updateHistoryDto)
    {
        return new()
        {
            Guid = updateHistoryDto.Guid,
            OvertimeGuid = updateHistoryDto.OvertimeGuid
        };
    }

    public static explicit operator HistoryDtoUpadate(History history)
    {
        return new()
        {
            Guid = history.Guid,
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
