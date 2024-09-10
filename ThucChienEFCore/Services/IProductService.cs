using ThucChienEFCore.DTOs;
using ThucChienEFCore.Models;

namespace ThucChienEFCore.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(int id);
    }
}
