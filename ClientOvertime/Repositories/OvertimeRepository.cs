using System.Text;
using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.Controllers;
using ClientOvertime.ViewModels.Overtimes;
using Newtonsoft.Json;

namespace ClientOvertime.Repositories;

public class OvertimeRepository : GeneralRepository<OvertimeVMRequest, Guid>, IOvertimeRepository
{
	public OvertimeRepository(string request = "overtimes/dummy/") : base(request)
	{
	}

	public async Task<ResponseHandler<IEnumerable<OvertimeVMRequest>>> GetOverManager(Guid guid)
	{

		ResponseHandler<IEnumerable<OvertimeVMRequest>> entityVM = null;
		using (var response = await _httpClient.GetAsync(_request + "manager/" + guid))
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<OvertimeVMRequest>>>(apiResponse);
		};
		return entityVM;
	}

	public async Task<ResponseHandler<ApprovalManager>> PutApproval(ApprovalManager entity)
    {
        ResponseHandler<ApprovalManager> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PutAsync(_request + "update", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<ApprovalManager>>(apiResponse);
        }
        return entityVM;
    }
}
