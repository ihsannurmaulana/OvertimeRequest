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
            return NotFound(new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<PayslipDtoGet>>
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
            return NotFound(new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<PayslipDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpPost]
    public IActionResult Create(PayslipDtoCreate newPayslip)
    {
        var payslip = _payslipService.CreatePayslip(newPayslip);
        if (payslip is null)
        {
            return BadRequest(new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Unsuccessfull to Create"
            });
        }

        return Ok(new ResponseHandler<PayslipDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfull to Create"
        });

    }

    [HttpPut("dummy")]
    public IActionResult Update(PayslipDtoUpdate updatePayslip)
    {
        var payslip = _payslipService.UpdatePayslips(updatePayslip);
        if (payslip == -1)
        {
            return NotFound(new ResponseHandler<PayslipDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id Not Found"
            });
        }
        else if (payslip == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<PayslipDtoUpdate>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to retrieving data from database"
            });
        }
        else
        {
            return Ok(new ResponseHandler<PayslipDtoUpdate>
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
            return NotFound(new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id Not Found"
            });
        }
        else if (payslip == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to retrieving data from database"
            });
        }
        else
        {
            return Ok(new ResponseHandler<PayslipDtoGet>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull to Delete"
            });
        }
    }
    [HttpGet("dummy")]
    public IActionResult GetPayslipOver()
    {
        var payslip = _payslipService.GetAllMasterOver();
        if (!payslip.Any())
        {
            return NotFound(new ResponseHandler<PayslipDtoGetMaster>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<PayslipDtoGetMaster>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpGet("dummy/{guid}")]
    public IActionResult GetPayslipOver(Guid guid)
    {
        var payslip = _payslipService.GetAllMasterOverbyGuid(guid);
        if (!payslip.Any())
        {
            return NotFound(new ResponseHandler<PayslipDtoGetMaster>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<PayslipDtoGetMaster>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpGet("dummy/emp/{guid}")]
    public IActionResult GetPayslipOverEmp(Guid guid)
    {
        var payslip = _payslipService.GetAllMasterOverbyEmpGuid(guid);
        if (!payslip.Any())
        {
            return NotFound(new ResponseHandler<PayslipDtoGetMaster>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<PayslipDtoGetMaster>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = payslip
        });
    }

    [HttpGet("dummy/total-expense")]
    public IActionResult GetTotalSalaryExpense()
    {
        double totalExpense = _payslipService.GetTotalSalaryExpense();

        return Ok(new ResponseHandler<double>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Total Salary Expense Calculated",
            Data = totalExpense
        });
    }

}
