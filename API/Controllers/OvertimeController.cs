﻿using API.DTOs.Overtimes;
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
            return NotFound(new ResponseHandler<GetOvertimeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetOvertimeDTO>>
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
            return NotFound(new ResponseHandler<GetOvertimeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });

        return Ok(new ResponseHandler<GetOvertimeDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = over

        });
    }

    [HttpPost]
    public IActionResult Create(NewOvertimeDTO newOvertime)
    {
        var over = _service.CreateOvertime(newOvertime);
        if (over is null)
        {
            return BadRequest(new ResponseHandler<GetOvertimeDTO>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Unsuccesful to Create"
            });
        }

        return Ok(new ResponseHandler<GetOvertimeDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfull to Create",
            Data = over
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateOvertimeDto updateOvertime)
    {
        var over = _service.UpdateOvertime(updateOvertime);
        if (over == -1)
        {
            return NotFound(new ResponseHandler<UpdateOvertimeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Not Found!!"
            });
        }
        else if (over == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<UpdateOvertimeDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to retrieving data from database"
            });
        }
        else
        {
            return Ok(new ResponseHandler<UpdateOvertimeDto>
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
            return NotFound(new ResponseHandler<GetOvertimeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        else if (overDelete == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GetOvertimeDTO>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to delete data from the database"
            });
        }
        else return Ok(new ResponseHandler<GetOvertimeDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfull to delete"
        });

    }

}