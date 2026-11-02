using Domain.Categories;

namespace Domain.Products;

public class ProductCategory
{
    public ProductId ProductId { get; }
    public CategoryId CategoryId { get; }

    public Product? Product { get; } = null;
    public Category? Category { get; } = null;

    private ProductCategory(ProductId productId, CategoryId categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    public static ProductCategory New(ProductId productId, CategoryId categoryId)
        => new(productId, categoryId);
}