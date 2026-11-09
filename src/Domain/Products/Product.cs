namespace Domain.Products;

public class Product
{
    public ProductId Id { get; }
    public string Title { get; private set; }
    public DateTime CreatedAt { get; }

    public List<ProductSpecification>? Specifications { get; private set; } = [];

    public ICollection<ProductCategory>? ProductCategories { get; } = null;

    private Product(ProductId id, string title, DateTime createdAt)
        => (Id, Title, CreatedAt) = (id, title, createdAt);

    public static Product New(ProductId id, string title, List<ProductSpecification> specifications)
        => new(id, title, DateTime.UtcNow)
        {
            Specifications = specifications
        };

    public void UpdateTitle(string title)
    {
        Title = title;
    }
}