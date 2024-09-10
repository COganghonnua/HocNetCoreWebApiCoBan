using Microsoft.EntityFrameworkCore;
using ThucChienEFCore.Access;
using ThucChienEFCore.Models;

namespace ThucChienEFCore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                await _dbContext.Category.AddAsync(category);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể thêm danh mục", ex);
            }
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            try
            {
                _dbContext.Category.Remove(category);
                await _dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new Exception("Không thể xóa được !",ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Category.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Không tìm thấy danh mục");
            }
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _dbContext.Category.FindAsync(category.Id);
            if (existingCategory == null)
            {
                throw new Exception("Không tìm thấy danh mục để cập nhật");
            }

            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
