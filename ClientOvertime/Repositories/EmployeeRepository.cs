using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Employees;
using Newtonsoft.Json;

namespace ClientOvertime.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeVM, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request) { }

    public async Task<ResponseHandler<IEnumerable<EmployeeVM>>> GetEmployees()
    {
        ResponseHandler<IEnumerable<EmployeeVM>> entityVM = null;
        using (var response = await _httpClient.GetAsync(_request + "get-all-employee"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeVM>>>(apiResponse);
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

    public async Task<ResponseHandler<int>> GetCount()
    {
        ResponseHandler<int> entity = null;
        using (var response = await _httpClient.GetAsync(_request + "total-count"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<int>>(apiResponse);
        }
        return entity;
    }


}
