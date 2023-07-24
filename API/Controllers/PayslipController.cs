using API.DTOs.Payslips;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/payslips")]
public class PayslipController : ControllerBase
{
    private readonly PayslipService _payslipService;

    public PayslipController(PayslipService payslipService)
    {
        _payslipService = payslipService;
    }

    [HttpGet]
    public IActionResult GetPayslip()
    {
        var payslip = _payslipService.GetPayslip();
        if (!payslip.Any())
        {
            return NotFound(new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetPayslipDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetPayslipByGuid(Guid guid)
    {
        var payslip = _payslipService.GetPayslipDtoByGuid(guid);
        if (payslip is null)
        {
            return NotFound(new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<GetPayslipDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpPost]
    public IActionResult Create(NewPayslipDto newPayslip)
    {
        var payslip = _payslipService.CreatePayslip(newPayslip);
        if (payslip is null)
        {
            return BadRequest(new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Unsuccessfull to Create"
            });
        }

        return Ok(new ResponseHandler<GetPayslipDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfull to Create"
        });

    }

    [HttpPut]
    public IActionResult Update(UpdatePayslip updatePayslip)
    {
        var payslip = _payslipService.UpdatePayslips(updatePayslip);
        if (payslip == -1)
        {
            return NotFound(new ResponseHandler<UpdatePayslip>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id Not Found"
            });
        }
        else if (payslip == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<UpdatePayslip>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to retrieving data from database"
            });
        }
        else
        {
            return Ok(new ResponseHandler<UpdatePayslip>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull to Update"
            });
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var payslip = _payslipService.DeletePayslip(guid);
        if (payslip == -1)
        {
            return NotFound(new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id Not Found"
            });
        }
        else if (payslip == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to retrieving data from database"
            });
        }
        else
        {
            return Ok(new ResponseHandler<GetPayslipDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull to Delete"
            });
        }
    }

}
