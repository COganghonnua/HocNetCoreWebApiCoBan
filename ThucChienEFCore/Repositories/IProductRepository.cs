using ThucChienEFCore.DTOs;
using ThucChienEFCore.Models;

namespace ThucChienEFCore.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> AddAsync(Product product);
        Task<ProductDto> UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
