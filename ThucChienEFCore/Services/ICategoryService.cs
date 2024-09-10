using System.Linq.Expressions;
using ThucChienEFCore.Models;

namespace ThucChienEFCore.Services
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task RemoveAsync(int id);
    }
}
