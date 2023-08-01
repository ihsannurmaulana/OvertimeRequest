using API.DTOs.Overtimes;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;


[ApiController]
[Route("api/overtimes")]
public class OvertimeController : ControllerBase
{
	private readonly OvertimeService _service;

	public OvertimeController(OvertimeService service)
	{
		_service = service;
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var over = _service.GetOvertime();

		if (!over.Any())
		{
			return NotFound(new ResponseHandler<OvertimeDtoGet>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		return Ok(new ResponseHandler<IEnumerable<OvertimeDtoGet>>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data found",
			Data = over
		});
	}

	[HttpGet("{guid}")]
	public IActionResult GetOvertimeByGuid(Guid guid)
	{
		var over = _service.GetOvertimeByGuid(guid);
		if (over is null)
			return NotFound(new ResponseHandler<OvertimeDtoGet>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});

		return Ok(new ResponseHandler<OvertimeDtoGet>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data Found",
			Data = over

		});
	}

	[HttpPost]
	public IActionResult Create(OvertimeDtoCreate newOvertime)
	{
		var over = _service.CreateOvertime(newOvertime);
		if (over is null)
		{
			return BadRequest(new ResponseHandler<OvertimeDtoGet>
			{
				Code = StatusCodes.Status400BadRequest,
				Status = HttpStatusCode.BadRequest.ToString(),
				Message = "Unsuccesful to Create"
			});
		}

		return Ok(new ResponseHandler<OvertimeDtoGet>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfull to Create",
			Data = over
		});
	}

	[HttpPut]
	public IActionResult Update(OvertimeDtoUpdate updateOvertime)
	{
		var over = _service.UpdateOvertime(updateOvertime);
		if (over == -1)
		{
			return NotFound(new ResponseHandler<OvertimeDtoUpdate>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "ID Not Found!!"
			});
		}
		else if (over == 0)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<OvertimeDtoUpdate>
			{
				Code = StatusCodes.Status500InternalServerError,
				Status = HttpStatusCode.InternalServerError.ToString(),
				Message = "Error to retrieving data from database"
			});
		}
		else
		{
			return Ok(new ResponseHandler<OvertimeDtoUpdate>
			{
				Code = StatusCodes.Status200OK,
				Status = HttpStatusCode.OK.ToString(),
				Message = "Successfull to update",
			});
		}

	}

	[HttpDelete]
	public IActionResult Delete(Guid guid)
	{
		var overDelete = _service.DeleteOvertime(guid);
		if (overDelete == -1)
		{
			return NotFound(new ResponseHandler<OvertimeDtoGet>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}
		else if (overDelete == 0)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<OvertimeDtoGet>
			{
				Code = StatusCodes.Status500InternalServerError,
				Status = HttpStatusCode.InternalServerError.ToString(),
				Message = "Error to delete data from the database"
			});
		}
		else return Ok(new ResponseHandler<OvertimeDtoGet>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfull to delete"
		});

	}

	[HttpGet("dummy")]
	public IActionResult GetAllDummy()
	{
		var over = _service.GetAllDummy();

		if (!over.Any())
		{
			return NotFound(new ResponseHandler<OvertimeRemainingDto>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		return Ok(new ResponseHandler<IEnumerable<OvertimeRemainingDto>>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data found",
			Data = over
		});
	}

	[HttpPost("dummy")]
	public IActionResult CreateDum(OvertimeCreateDummyDto newOvertime)
	{

		var over = _service.CreateDummy(newOvertime);
		if (over is null)
		{
			return BadRequest(new ResponseHandler<OvertimeRemainingDto>
			{
				Code = StatusCodes.Status400BadRequest,
				Status = HttpStatusCode.BadRequest.ToString(),
				Message = "Unsuccesful to Create"
			});
		}

		return Ok(new ResponseHandler<OvertimeRemainingDto>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfull to Create",
			Data = over
		});
	}

	[HttpGet("emp/{guid}")]
	public IActionResult GetAllByGuidEmp(Guid guid)
	{
		var over = _service.GetAllByGuidEmp(guid);
		if (over is null)
		{
			return NotFound(new ResponseHandler<OvertimeRemainingDto>
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}
		return Ok(new ResponseHandler<IEnumerable<OvertimeRemainingDto>>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data Found",
			Data = over
		});
	}

}