namespace Domain.Products;

public record ProductImageId(Guid Value)
{
    public static ProductImageId Empty() => new(Guid.Empty);
    public static ProductImageId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}