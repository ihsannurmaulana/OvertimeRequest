using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace ClientOvertime.Repositories;

public class GeneralRepository<TEntity, TId> : IGeneralRepository<TEntity, TId>
    where TEntity : class
{
    protected readonly string _request;
    protected readonly HttpClient _httpClient;
    protected readonly IHttpContextAccessor contextAccessor;

    public GeneralRepository(string request)
    {
        _request = request;
        contextAccessor = new HttpContextAccessor();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7104/api/")
        };
    }

    public async Task<ResponseHandler<IEnumerable<TEntity>>> Get()
    {
        ResponseHandler<IEnumerable<TEntity>> entity = null;
        using (var response = await _httpClient.GetAsync(_request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<TEntity>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Get(TId id)
    {
        ResponseHandler<TEntity> entity = null;
        using (var response = await _httpClient.GetAsync(_request + id))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Post(TEntity entity)
    {
        ResponseHandler<TEntity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PostAsync(_request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseHandler<TEntity>> Put(TId id, TEntity entity)
    {
        ResponseHandler<TEntity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PutAsync(_request + "?guid=" + id, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseHandler<TEntity>> Delete(TId id)
    {
        ResponseHandler<TEntity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        using (var response = _httpClient.DeleteAsync(_request + "?guid=" + id).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityVM;
    }
}
