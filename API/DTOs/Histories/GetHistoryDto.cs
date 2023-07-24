using API.Models;

namespace API.DTOs.Histories;

public class GetHistoryDto
{
    public Guid Guid { get; set; }

    public Guid OvertimeGuid { get; set; }

    public static implicit operator History(GetHistoryDto getHistoryDto)
    {
        return new()
        {
            Guid = getHistoryDto.Guid,
            OvertimeGuid = getHistoryDto.OvertimeGuid
        };
    }

    public static explicit operator GetHistoryDto(History history)
    {
        return new()
        {
            Guid = history.Guid,
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
