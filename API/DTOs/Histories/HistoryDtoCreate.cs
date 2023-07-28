using API.Models;

namespace API.DTOs.Histories;

public class HistoryDtoCreate
{
    public Guid OvertimeGuid { get; set; }


    public static implicit operator History(HistoryDtoCreate newHistoryDto)
    {
        return new()
        {
            OvertimeGuid = newHistoryDto.OvertimeGuid,
        };
    }

    public static explicit operator HistoryDtoCreate(History history)
    {
        return new()
        {
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
