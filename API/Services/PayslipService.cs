using API.Contracts;
using API.DTOs.Payslips;
using API.Models;

namespace API.Services;

public class PayslipService
{
    private readonly IPayslipRepository _payslipRepository;

    public PayslipService(IPayslipRepository payslipRepository)
    {
        _payslipRepository = payslipRepository;
    }

    public IEnumerable<GetPayslipDto> GetPayslip()
    {
        var payslip = _payslipRepository.GetAll().ToList();
        if (!payslip.Any())
        {
            return Enumerable.Empty<GetPayslipDto>();
        }
        List<GetPayslipDto> payslipDtos = new();

        foreach (var pays in payslip)
        {
            payslipDtos.Add((GetPayslipDto)pays);
        }

        return payslipDtos;
    }

    public GetPayslipDto? GetPayslipDtoByGuid(Guid guid)
    {
        var payslip = _payslipRepository.GetByGuid(guid);
        if (payslip == null)
        {
            return null;
        }

        return (GetPayslipDto)payslip;
    }

    public GetPayslipDto? CreatePayslip(NewPayslipDto newPayslip)
    {
        Payslip payslips = newPayslip;
        var payslip = _payslipRepository.Create(payslips);
        if (payslip == null) return null;

        return (GetPayslipDto)payslip;
    }

    public int UpdatePayslips(UpdatePayslip updatePayslip)
    {
        var payslipps = _payslipRepository.GetByGuid(updatePayslip.Guid);
        if (payslipps == null)
        {
            return -1;
        }
        var isUpdate = _payslipRepository.Update(updatePayslip);
        return !isUpdate ? 0 : 1;
    }

    public int DeletePayslip(Guid guid)
    {
        var payslip = _payslipRepository.GetByGuid(guid);
        if (payslip == null) { return -1; }

        var isDelete = _payslipRepository.Delete(payslip);
        return !isDelete ? 0 : 1;
    }
}
