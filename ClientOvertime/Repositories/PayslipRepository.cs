using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Payslips;
using Newtonsoft.Json;
using System.Text;

namespace ClientOvertime.Repositories;

public class PayslipRepository : GeneralRepository<PayslipVMGet, Guid>, IPayslipRepository
{
    public PayslipRepository(string request = "payslips/dummy") : base(request)
    {
    }
    public async Task<ResponseHandler<PayslipVMGet>> GetDetail(Guid guid)
    {
        ResponseHandler<PayslipVMGet> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entityVM), Encoding.UTF8, "application/json");
        using (var response = _httpClient.GetAsync(_request + $"/{guid}").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<PayslipVMGet>>(apiResponse);
        }
        return entityVM;
    }
}
