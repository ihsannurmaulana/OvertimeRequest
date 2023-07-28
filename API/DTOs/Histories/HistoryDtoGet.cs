using API.Models;

namespace API.DTOs.Histories;

public class HistoryDtoGet
{
    public Guid Guid { get; set; }

    public Guid OvertimeGuid { get; set; }

    public static implicit operator History(HistoryDtoGet getHistoryDto)
    {
        return new()
        {
            Guid = getHistoryDto.Guid,
            OvertimeGuid = getHistoryDto.OvertimeGuid
        };
    }

    public static explicit operator HistoryDtoGet(History history)
    {
        return new()
        {
            Guid = history.Guid,
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
