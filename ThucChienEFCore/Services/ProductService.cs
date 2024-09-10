using ThucChienEFCore.DTOs;
using ThucChienEFCore.Models;
using ThucChienEFCore.Repositories;
using ThucChienEFCore.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        var product = MapToProduct(productDto);
        return await _productRepository.AddAsync(product);
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
    {
        var product = MapToProduct(productDto);
        return await _productRepository.UpdateAsync(product);
    }

    private Product MapToProduct(ProductDto productDto)
    {
        return new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            OriginalPrice = productDto.OriginalPrice,
            DiscountedPrice = productDto.DiscountedPrice,
            Description = productDto.Description,
            Quantity = productDto.Quantity,
            ImageData = productDto.ImageData,
            CategoryId = productDto.CategoryId
        };
    }

    public async Task DeleteProductAsync(int id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct != null)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
