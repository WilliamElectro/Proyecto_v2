using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface IProductsUnitOfWork
    {
        Task<Response<Product>> GetAsync(int id);

        Task<Response<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<Response<Product>> AddFullAsync(ProductDTO productDTO);

        Task<Response<Product>> UpdateFullAsync(ProductDTO productDTO);

        Task<Response<ImageDTO>> AddImageAsync(ImageDTO imageDTO);

        Task<Response<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO);

        Task<Response<Product>> UpdateAsync(Product product);
    }
}