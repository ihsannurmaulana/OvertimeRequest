using API.Models;

namespace API.DTOs.Histories;

public class NewHistoryDto
{
    public Guid OvertimeGuid { get; set; }


    public static implicit operator History(NewHistoryDto newHistoryDto)
    {
        return new()
        {
            OvertimeGuid = newHistoryDto.OvertimeGuid,
        };
    }

    public static explicit operator NewHistoryDto(History history)
    {
        return new()
        {
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
