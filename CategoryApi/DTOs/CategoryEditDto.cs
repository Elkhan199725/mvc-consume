namespace CategoryApi.DTOs;

public sealed class CategoryEditDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
