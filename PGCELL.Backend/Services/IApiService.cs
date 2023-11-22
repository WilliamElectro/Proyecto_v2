using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Services
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}