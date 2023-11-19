using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Helpers
{
    public interface IOrdersHelper
    {
        Task<Response<bool>> ProcessOrderAsync(string email, string remarks);
    }
}