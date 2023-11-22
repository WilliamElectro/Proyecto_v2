using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface IOrdersUnitOfWork
    {
        Task<Response<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<Response<Order>> GetAsync(int id);

        Task<Response<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);

        Task<Response<Order>> AddAsync(Order order);
    }
}