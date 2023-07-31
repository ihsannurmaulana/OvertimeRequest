using API.Contracts;
using API.DTOs.Overtimes;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class OvertimeService
{
    private readonly IOvertimeRepository _overtimeRepository;

    public OvertimeService(IOvertimeRepository overtimeRepository)
    {
        _overtimeRepository = overtimeRepository;
    }

    public IEnumerable<OvertimeDtoGet> GetOvertime()
    {
        var overtime = _overtimeRepository.GetAll().ToList();
        if (!overtime.Any()) return Enumerable.Empty<OvertimeDtoGet>();
        List<OvertimeDtoGet> overtimeDTO = new();

        foreach (var over in overtime)
        {
            overtimeDTO.Add((OvertimeDtoGet)over);
        }

        return overtimeDTO;
    }

    public OvertimeDtoGet? GetOvertimeByGuid(Guid guid)
    {
        var overtime = _overtimeRepository.GetByGuid(guid);
        if (overtime is null) return null;

        return (OvertimeDtoGet)overtime;
    }

    public OvertimeDtoGet? CreateOvertime(OvertimeDtoCreate newOvertime)
    {
        Overtime overtime = newOvertime;
        overtime.OvertimeNumber = GenerateHandler.OverNumber(_overtimeRepository.GetLastOvertimeNumber());
        var createdOvertime = _overtimeRepository.Create(overtime);
        if (createdOvertime is null) return null;

        return (OvertimeDtoGet)createdOvertime;
    }

    public int UpdateOvertime(OvertimeDtoUpdate updOvertime)
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


    public OvertimeRemainingDto? CreateDummy(OvertimeCreateDummyDto over)
    {
        Overtime overtime = over;
        if (overtime.Status == 0)
        {
            overtime.OvertimeNumber = GenerateHandler.OverNumber(_overtimeRepository.GetLastOvertimeNumber());
            overtime.StartDate = over.StartDate;
            overtime.EndDate = over.EndDate;
            var createdOver = _overtimeRepository.CreateOver(overtime);
            if (createdOver is null) return null;

            return (OvertimeRemainingDto)createdOver;
        }
        return null;

    }

    public IEnumerable<OvertimeRemainingDto>? GetAllDummy()
    {
        var overtime = _overtimeRepository.GetAll().ToList();
        if (!overtime.Any()) return Enumerable.Empty<OvertimeRemainingDto>();
        List<OvertimeRemainingDto> overtimeDTO = new();

        foreach (var over in overtime)
        {
            overtimeDTO.Add((OvertimeRemainingDto)over);
        }

        return overtimeDTO;
    }


}