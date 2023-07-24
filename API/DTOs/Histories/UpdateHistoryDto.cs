using API.Models;

namespace API.DTOs.Histories;

public class UpdateHistoryDto
{
    public Guid Guid { get; set; }

    public Guid OvertimeGuid { get; set; }

    public static implicit operator History(UpdateHistoryDto updateHistoryDto)
    {
        return new()
        {
            Guid = updateHistoryDto.Guid,
            OvertimeGuid = updateHistoryDto.OvertimeGuid
        };
    }

    public static explicit operator UpdateHistoryDto(History history)
    {
        return new()
        {
            Guid = history.Guid,
            OvertimeGuid = history.OvertimeGuid
        };
    }
}
