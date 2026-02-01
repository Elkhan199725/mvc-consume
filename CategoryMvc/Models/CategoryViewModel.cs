namespace CategoryMvc.Models;

public class CategoryViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
