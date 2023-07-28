using API.DTOs.Histories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;


[ApiController]
[Route("api/histories")]
public class HistoryController : ControllerBase
{
    private readonly HistoryService _service;

    public HistoryController(HistoryService service)
    {
        _service = service;
    }

    [HttpGet("get-all-history-user")]
    public IActionResult GetAllHistory()
    {
        var entities = _service.GetAllHistories();

        if (!entities.Any())
            return NotFound(new ResponseHandler<GetAllHistoryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<IEnumerable<GetAllHistoryDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetHistory();

        if (!entities.Any())
            return NotFound(new ResponseHandler<HistoryDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<IEnumerable<HistoryDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var history = _service.GetHistoryByGuid(guid);
        if (history is null)
            return NotFound(new ResponseHandler<HistoryDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<HistoryDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = history
        });
    }

    [HttpPost]
    public IActionResult Create(HistoryDtoCreate newHistoryDto)
    {
        var createdHistory = _service.CreateHistory(newHistoryDto);
        if (createdHistory is null)
            return BadRequest(new ResponseHandler<HistoryDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });

        return Ok(new ResponseHandler<HistoryDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdHistory
        });
    }

    [HttpPut]
    public IActionResult Update(HistoryDtoUpadate updateHistoryDto)
    {
        var update = _service.UpdateHistory(updateHistoryDto);
        if (update is -1)
            return NotFound(new ResponseHandler<HistoryDtoUpadate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });

        if (update is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<HistoryDtoUpadate>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<HistoryDtoUpadate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteHistory(guid);

        if (delete is -1)
            return NotFound(new ResponseHandler<HistoryDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID not found"
            });

        if (delete is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<HistoryDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<HistoryDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }
}
