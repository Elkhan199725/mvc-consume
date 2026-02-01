using System.Net.Http.Json;
using CategoryMvc.Models;

namespace CategoryMvc.Services;

public sealed class CategoryApiService : ICategoryApiService
{
    private readonly HttpClient _httpClient;

    public CategoryApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("api/Category");
        
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryViewModel>>();
        return categories ?? [];
    }

    public async Task<CategoryViewModel?> GetCategoryByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/Category/{id}");
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
    }

    public async Task<CategoryViewModel?> CreateCategoryAsync(CategoryCreateModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Category", model);
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
    }

    public async Task<bool> UpdateCategoryAsync(Guid id, CategoryEditModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Category/{id}", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Category/{id}");
        return response.IsSuccessStatusCode;
    }
}
