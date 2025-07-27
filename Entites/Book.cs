namespace Task.Api.Entites;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public string Author { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
