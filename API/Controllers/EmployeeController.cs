using API.DTOs.Employees;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;


[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet("get-manager")]
    public IActionResult GetManagers()
    {
        var entities = _service.GetManager();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<EmployeeDtoGetAll>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGetAll>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("get-all-employee")]
    public IActionResult GetAllEmployee()
    {
        var entities = _service.GetAllMaster();

        if (!entities.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGetAll>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGetAll>>
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
        var entities = _service.GetEmployee();

        if (!entities.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("get-manager/{guid}")]
    public IActionResult GetMangerByGuid(Guid guid)
    {
        var employee = _service.GetManagerByGuid(guid);
        if (employee is null)
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = employee
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _service.GetEmployeeByGuid(guid);
        if (employee is null)
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = employee
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeeDtoCreate newEmployeeDto)
    {
        var createdEmployee = _service.CreateEmployee(newEmployeeDto);
        if (createdEmployee is null)
            return BadRequest(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdEmployee
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeeDtoUpdate updateEmployeeDto)
    {
        var update = _service.UpdateEmployee(updateEmployeeDto);
        if (update is -1)
            return NotFound(new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID not found"
            });

        if (update is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<EmployeeDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteEmployee(guid);

        if (delete is -1)
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });

        if (delete is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }


    [HttpGet("total-count")]
    public IActionResult GetTotalEmployeeCount()
    {
        int totalEmployeeCount = _service.GetTotalEmployeeCount();

        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Total Employee Count",
            Data = totalEmployeeCount
        });
    }
}
