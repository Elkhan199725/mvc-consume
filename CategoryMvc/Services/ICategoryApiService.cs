namespace CategoryMvc.Services;

using CategoryMvc.Models;

public interface ICategoryApiService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
    Task<CategoryViewModel?> GetCategoryByIdAsync(Guid id);
    Task<CategoryViewModel?> CreateCategoryAsync(CategoryCreateModel model);
    Task<bool> UpdateCategoryAsync(Guid id, CategoryEditModel model);
    Task<bool> DeleteCategoryAsync(Guid id);
}
