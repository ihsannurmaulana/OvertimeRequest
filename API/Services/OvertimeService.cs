using API.Contracts;
using API.DTOs.Overtimes;
using API.Models;

namespace API.Services;

public class OvertimeService
{
    private readonly IOvertimeRepository _overtimeRepository;

    public OvertimeService(IOvertimeRepository overtimeRepository)
    {
        _overtimeRepository = overtimeRepository;
    }

    public IEnumerable<GetOvertimeDTO> GetOvertime()
    {
        var overtime = _overtimeRepository.GetAll().ToList();
        if (!overtime.Any()) return Enumerable.Empty<GetOvertimeDTO>();
        List<GetOvertimeDTO> overtimeDTO = new();

        foreach (var over in overtime)
        {
            overtimeDTO.Add((GetOvertimeDTO)over);
        }

        return overtimeDTO;
    }

    public GetOvertimeDTO? GetOvertimeByGuid(Guid guid)
    {
        var overtime = _overtimeRepository.GetByGuid(guid);
        if (overtime is null) return null;

        return (GetOvertimeDTO)overtime;
    }

    public GetOvertimeDTO? CreateOvertime(NewOvertimeDTO newOvertime)
    {
        Overtime overtime = newOvertime;
        var createdOvertime = _overtimeRepository.Create(overtime);
        if (createdOvertime is null) return null;

        return (GetOvertimeDTO)createdOvertime;
    }

    public int UpdateOvertime(UpdateOvertimeDto updOvertime)
    {
        var getOvertime = _overtimeRepository.GetByGuid(updOvertime.Guid);
        if (getOvertime is null) return -1;

        var isUpdate = _overtimeRepository.Update(updOvertime);
        return !isUpdate ? 0 : 1;
    }


    public int DeleteOvertime(Guid guid)
    {
        var overtime = _overtimeRepository.GetByGuid(guid);
        if (overtime is null) return -1;

        var isDelete = _overtimeRepository.Delete(overtime);
        return !isDelete ? 0 : 1;
    }

}