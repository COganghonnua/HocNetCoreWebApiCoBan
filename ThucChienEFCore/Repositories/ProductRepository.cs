using Microsoft.EntityFrameworkCore;
using ThucChienEFCore.Access;
using ThucChienEFCore.DTOs;
using ThucChienEFCore.Models;
using ThucChienEFCore.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await _context.Product
            .Include(p => p.Category)
            .AsNoTracking()
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                OriginalPrice = p.OriginalPrice,
                DiscountedPrice = p.DiscountedPrice,
                Description = p.Description,
                Quantity = p.Quantity,
                ImageData = p.ImageData,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : "Không có Category"
            }).ToListAsync();
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _context.Product
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            OriginalPrice = product.OriginalPrice,
            DiscountedPrice = product.DiscountedPrice,
            Description = product.Description,
            Quantity = product.Quantity,
            ImageData = product.ImageData,
            CategoryId = product.CategoryId,
            CategoryName = product.Category != null ? product.Category.Name : "Không có Category"
        };
    }

    public async Task<ProductDto> AddAsync(Product product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();

        var createdProduct = await _context.Product
               .Include(p => p.Category)
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.Id == product.Id);

        return new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            OriginalPrice = createdProduct.OriginalPrice,
            DiscountedPrice = createdProduct.DiscountedPrice,
            Description = createdProduct.Description,
            Quantity = createdProduct.Quantity,
            ImageData = createdProduct.ImageData,
            CategoryId = createdProduct.CategoryId,
            CategoryName = createdProduct.Category != null ? createdProduct.Category.Name : "Không tìm thấy category"
        };
    }

    public async Task<ProductDto> UpdateAsync(Product product)
    {
        var existingProduct = await _context.Product.FindAsync(product.Id);
        if (existingProduct == null) return null;

        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();

        return new ProductDto
        {
            Id = existingProduct.Id,
            Name = existingProduct.Name,
            OriginalPrice = existingProduct.OriginalPrice,
            DiscountedPrice = existingProduct.DiscountedPrice,
            Description = existingProduct.Description,
            Quantity = existingProduct.Quantity,
            ImageData = existingProduct.ImageData,
            CategoryId = existingProduct.CategoryId,
        };
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product != null)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
  
}
