using API.Contracts;
using API.DTOs.Overtimes;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class OvertimeService
{
	private readonly IOvertimeRepository _overtimeRepository;
	private readonly IEmployeeRepository _employeeRepository;

	public OvertimeService(IOvertimeRepository overtimeRepository, IEmployeeRepository employeeRepository)
	{
		_overtimeRepository = overtimeRepository;
		_employeeRepository = employeeRepository;
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
		overtime.OvertimeNumber = GenerateHandler.OverNumber(_overtimeRepository.GetLastOvertimeNumber());
		overtime.StartDate = over.StartDate;
		overtime.EndDate = over.EndDate;
		var createdOver = _overtimeRepository.CreateOver(overtime);
		if (createdOver is null) return null;

		return (OvertimeRemainingDto)createdOver;
	}

	public IEnumerable<OvertimeRemainingDto>? GetAllDummy()
	{

		var master = (from o in _overtimeRepository.GetAll()
					  join e in _employeeRepository.GetAll() on o.EmployeeGuid equals e.Guid
					  select new OvertimeRemainingDto
					  {
						  Guid = o.Guid,
						  FullName = e.FirstName + " " + e.LastName,
						  EmployeeGuid = e.Guid,
						  EndDate = o.EndDate,
						  OvertimeNumber = o.OvertimeNumber,
						  Paid = o.PaidOvertime,
						  Remaining = o.Remaining,
						  Remarks = o.Remarks,
						  StartDate = o.StartDate,
						  Status = o.Status,
						  CreatedDate = DateTime.Now
					  }).ToList();
		return master;

	}

	public IEnumerable<OvertimeRemainingDto>? GetAllByGuidEmp(Guid guid)
	{
		var employee = _employeeRepository.GetByGuid(guid);
		var overtime = _overtimeRepository.ListRemainingOvertime(employee.Guid);
		if (overtime is null) return null;

		return overtime;
	}
	public IEnumerable<OvertimeRemainingDto>? GetAllByGuidManager(Guid guid)
	{
		var manager = _employeeRepository.GetByGuid(guid);
		var overtime = _overtimeRepository.ListRemainingOvertimeByManagerGuid(manager.Guid);
		if (overtime is null) return null;
		return overtime;
	}
	public int UpdateStatusRemaining(OvertimeUpdateStatus overtime)
	{
		var over = _overtimeRepository.GetByGuid(overtime.Guid);
		if (over is null) return -1;
		over.Guid = overtime.Guid;
		over.Status = overtime.Status;
		var isUpdate = _overtimeRepository.Update(over);
		return !isUpdate ? 0 : 1;

	}

}