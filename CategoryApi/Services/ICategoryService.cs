using CategoryApi.DTOs;

namespace CategoryApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
    Task<CategoryReadDto?> GetCategoryByIdAsync(Guid id);
    Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
    Task<bool> UpdateCategoryAsync(Guid id, CategoryEditDto categoryEditDto);
    Task<bool> DeleteCategoryAsync(Guid id);
}
