using API.Contracts;
using API.DTOs.Histories;

namespace API.Services;

public class HistoryService
{
    private readonly IHistoryRepository _historyRepository;

    public HistoryService(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public IEnumerable<GetHistoryDto> GetHistory()
    {
        var histories = _historyRepository.GetAll().ToList();
        if (!histories.Any()) return Enumerable.Empty<GetHistoryDto>(); // No histories found
        List<GetHistoryDto> historyDtos = new();

        foreach (var history in histories)
        {
            historyDtos.Add((GetHistoryDto)history);
        }

        return historyDtos; // Histories found
    }

    public GetHistoryDto? GetHistoryByGuid(Guid guid)
    {
        var history = _historyRepository.GetByGuid(guid);

        if (history is null) return null;

        return (GetHistoryDto)history;
    }

    public GetHistoryDto? CreateHistory(NewHistoryDto newHistoryDto)
    {
        var createdHistory = _historyRepository.Create(newHistoryDto);
        if (createdHistory is null) return null;

        return (GetHistoryDto)createdHistory;
    }

    public int UpdateHistory(UpdateHistoryDto updateHistoryDto)
    {
        var getHistory = _historyRepository.GetByGuid(updateHistoryDto.Guid);

        if (getHistory is null) return -1;

        var isUpdated = _historyRepository.Update(updateHistoryDto);
        return !isUpdated ? 0 :
            1;
    }

    public int DeleteHistory(Guid guid)
    {
        var history = _historyRepository.GetByGuid(guid);

        if (history is null) return -1;

        var isDeleted = _historyRepository.Delete(history);
        return !isDeleted ? 0 :
            1;
    }
}
