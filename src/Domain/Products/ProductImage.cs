namespace Domain.Products;

public class ProductImage
{
    public ProductImageId Id { get; }
    public string FileName { get; }

    public string FilePath => $"images/{Id}.jpeg";

    private ProductImage(ProductImageId id, string fileName)
    {
        Id = id;
        FileName = fileName;
    }

    public static ProductImage New(ProductImageId id, string fileName)
        => new(id, fileName);
}