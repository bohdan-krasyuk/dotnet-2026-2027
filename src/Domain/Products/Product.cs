namespace Domain.Products;

public class Product
{
    public ProductId Id { get; }
    public string Title { get; private set; }
    public DateTime CreatedAt { get; }

    private Product(ProductId id, string title, DateTime createdAt)
        => (Id, Title, CreatedAt) = (id, title, createdAt);

    public static Product New(ProductId id, string title)
        => new(id, title, DateTime.UtcNow);

    public void UpdateName(string title)
    {
        Title = title;
    }
}