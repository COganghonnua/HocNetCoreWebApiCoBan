using ThucChienEFCore.Models;
using ThucChienEFCore.Repositories;

namespace ThucChienEFCore.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
           return  await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if(category != null)
            {
                await _categoryRepository.DeleteCategoryAsync(category);
            }
          
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException("Không tìm thấy danh mục");
            }

            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
