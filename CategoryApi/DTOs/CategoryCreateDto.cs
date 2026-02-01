namespace CategoryApi.DTOs;

public sealed class CategoryCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
