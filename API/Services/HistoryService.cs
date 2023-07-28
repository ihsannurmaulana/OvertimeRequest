using API.Contracts;
using API.DTOs.Histories;

namespace API.Services;

public class HistoryService
{
    private readonly IHistoryRepository _historyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOvertimeRepository _overtimeRepository;

    public HistoryService(IHistoryRepository historyRepository, IEmployeeRepository employeeRepository, IOvertimeRepository overtimeRepository)
    {
        _historyRepository = historyRepository;
        _employeeRepository = employeeRepository;
        _overtimeRepository = overtimeRepository;
    }

    // Get All History User
    public IEnumerable<GetAllHistoryDto> GetAllHistories()
    {
        var master = (from history in _historyRepository.GetAll()
                      join overtime in _overtimeRepository.GetAll() on history.OvertimeGuid equals overtime.Guid
                      join employee in _employeeRepository.GetAll() on overtime.EmployeeGuid equals employee.Guid
                      select new GetAllHistoryDto
                      {
                          Guid = history.Guid,
                          OvertimeNumber = overtime.OvertimeNumber,
                          FullName = employee.FirstName + " " + employee.LastName,
                          Nik = employee.Nik,
                          StartDate = overtime.StartDate,
                          EndDate = overtime.EndDate,
                          Submited = DateTime.Now,
                          Status = overtime.Status,
                          ManagerGuid = employee.ManagerGuid

                      }).ToList();

        foreach (var getDataEmployee in master)
        {
            if (getDataEmployee.ManagerGuid != Guid.Empty)
            {
                // Cari data manager berdasarkan ManagerGuid
                var manager = master.FirstOrDefault(e => e.Guid == getDataEmployee.ManagerGuid);
                if (manager != null)
                {
                    getDataEmployee.ApproveBy = $"{manager.Nik} - {manager.FullName}";
                }
            }
        }

        return master;
    }


    public IEnumerable<HistoryDtoGet> GetHistory()
    {
        var histories = _historyRepository.GetAll().ToList();
        if (!histories.Any()) return Enumerable.Empty<HistoryDtoGet>(); // No histories found
        List<HistoryDtoGet> historyDtos = new();

        foreach (var history in histories)
        {
            historyDtos.Add((HistoryDtoGet)history);
        }

        return historyDtos; // Histories found
    }

    public HistoryDtoGet? GetHistoryByGuid(Guid guid)
    {
        var history = _historyRepository.GetByGuid(guid);

        if (history is null) return null;

        return (HistoryDtoGet)history;
    }

    public HistoryDtoGet? CreateHistory(HistoryDtoCreate newHistoryDto)
    {
        var createdHistory = _historyRepository.Create(newHistoryDto);
        if (createdHistory is null) return null;

        return (HistoryDtoGet)createdHistory;
    }

    public int UpdateHistory(HistoryDtoUpadate updateHistoryDto)
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
