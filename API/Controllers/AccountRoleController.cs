using API.DTOs.AccountRoles;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;


[ApiController]
[Route("api/account-roles")]
public class AccountRoleController : ControllerBase
{
    private readonly AccountRoleService _service;

    public AccountRoleController(AccountRoleService service)
    {
        _service = service;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetAccountRole();

        if (!entities.Any())
        {
            return NotFound(new ResponseHandler<AccountRoleDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<AccountRoleDtoGet>>
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
        var accountRole = _service.GetAccountRoleByGuid(guid);
        if (accountRole is null)
            return NotFound(new ResponseHandler<AccountRoleDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });

        return Ok(new ResponseHandler<AccountRoleDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = accountRole
        });
    }

    [HttpPost]
    public IActionResult Create(AccountRoleDtoCreate newAccountRoleDto)
    {
        var createdAccountRole = _service.CreateAccountRole(newAccountRoleDto);
        if (createdAccountRole is null)
            return BadRequest(new ResponseHandler<AccountRoleDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });

        return Ok(new ResponseHandler<AccountRoleDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdAccountRole
        });
    }

    [HttpPut]
    public IActionResult Update(AccountRoleDtoUpdate updateAccountRoleDto)
    {
        var update = _service.UpdateAccountRole(updateAccountRoleDto);
        if (update is -1)
            return NotFound(new ResponseHandler<AccountRoleDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID not found"
            });

        if (update is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountRoleDtoUpdate>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<AccountRoleDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteAccountRole(guid);

        if (delete is -1)
            return NotFound(new ResponseHandler<AccountRoleDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });

        if (delete is 0)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountRoleDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<AccountRoleDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }

}