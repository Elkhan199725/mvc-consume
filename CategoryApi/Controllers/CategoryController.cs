using CategoryApi.DTOs;
using CategoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto);
        return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryEditDto categoryEditDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, categoryEditDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
