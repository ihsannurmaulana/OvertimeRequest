using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Employees;
using Newtonsoft.Json;

namespace ClientOvertime.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeVMRegister, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request) { }

    public async Task<ResponseHandler<IEnumerable<EmployeeVMRegister>>> GetEmployees()
    {
        ResponseHandler<IEnumerable<EmployeeVMRegister>> entityVM = null;
        using (var response = await _httpClient.GetAsync(_request + "get-all-employee"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeVMRegister>>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseHandler<IEnumerable<ManagerVM>>> GetManager()
    {
        ResponseHandler<IEnumerable<ManagerVM>> entityVM = null;
        using (var response = await _httpClient.GetAsync(_request + "get-manager"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ManagerVM>>>(apiResponse);
        }
        return entityVM;
    }


}
