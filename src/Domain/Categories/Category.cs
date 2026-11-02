using Domain.Products;

namespace Domain.Categories;

public class Category
{
    public CategoryId Id { get; }
    public string Name { get; }

    public ICollection<ProductCategory>? ProductCategories { get; } = null;

    private Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category New(CategoryId categoryId, string name)
        => new(categoryId, name);
}