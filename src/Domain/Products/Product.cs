namespace Domain.Products;

public class Product
{
    public ProductId Id { get; }
    public string Title { get; private set; }
    public ProductStatus Status { get; }
    public DateTime CreatedAt { get; }

    public List<ProductSpecification>? Specifications { get; private set; } = [];

    public ICollection<ProductCategory>? ProductCategories { get; } = null;

    private Product(ProductId id, string title, ProductStatus status, DateTime createdAt)
        => (Id, Title, Status, CreatedAt) = (id, title, status, createdAt);

    public static Product New(ProductId id, string title, List<ProductSpecification> specifications)
        => new(id, title, ProductStatus.Active, DateTime.UtcNow)
        {
            Specifications = specifications
        };

    public void UpdateTitle(string title)
    {
        Title = title;
    }
}