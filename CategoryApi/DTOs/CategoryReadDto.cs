namespace CategoryApi.DTOs;

public sealed class CategoryReadDto
{
    public Guid Id { get; set; }
    public required string Name {  get; set; }
    public string? Description { get; set; }
}
