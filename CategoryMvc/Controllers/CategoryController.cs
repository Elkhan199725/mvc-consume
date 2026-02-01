using CategoryMvc.Models;
using CategoryMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoryMvc.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryApiService _categoryApiService;

    public CategoryController(ICategoryApiService categoryApiService)
    {
        _categoryApiService = categoryApiService;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryApiService.GetAllCategoriesAsync();
        return View(categories);
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var category = await _categoryApiService.GetCategoryByIdAsync(id);
        
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _categoryApiService.CreateCategoryAsync(model);
        
        if (result == null)
        {
            ModelState.AddModelError(string.Empty, "Failed to create category. Please try again.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Category created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await _categoryApiService.GetCategoryByIdAsync(id);
        
        if (category == null)
        {
            return NotFound();
        }

        var model = new CategoryEditModel
        {
            Name = category.Name,
            Description = category.Description
        };

        ViewBag.CategoryId = id;
        return View(model);
    }

    // POST: Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CategoryEditModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CategoryId = id;
            return View(model);
        }

        var success = await _categoryApiService.UpdateCategoryAsync(id, model);
        
        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Failed to update category. Please try again.");
            ViewBag.CategoryId = id;
            return View(model);
        }

        TempData["SuccessMessage"] = "Category updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await _categoryApiService.GetCategoryByIdAsync(id);
        
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var success = await _categoryApiService.DeleteCategoryAsync(id);
        
        if (!success)
        {
            TempData["ErrorMessage"] = "Failed to delete category. Please try again.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        TempData["SuccessMessage"] = "Category deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
