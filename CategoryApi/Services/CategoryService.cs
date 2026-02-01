using CategoryApi.Data;
using CategoryApi.DTOs;
using CategoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<CategoryReadDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return null;

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
    {
        var category = new Category
        {
            Name = categoryCreateDto.Name,
            Description = categoryCreateDto.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public async Task<bool> UpdateCategoryAsync(Guid id, CategoryEditDto categoryEditDto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        category.Name = categoryEditDto.Name;
        category.Description = categoryEditDto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
