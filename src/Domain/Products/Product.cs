namespace Domain.Products;

public class Product
{
    public Guid Id { get; }
    public string Title { get; }
    public DateTime CreatedAt { get; }

    private Product(Guid id, string title, DateTime createdAt)
        => (Id, Title, CreatedAt) = (id, title, createdAt);

    public static Product New(Guid id, string title)
        => new(id, title, DateTime.UtcNow);
}