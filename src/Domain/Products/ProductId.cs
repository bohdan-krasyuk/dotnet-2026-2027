namespace Domain.Products;

public record ProductId(Guid Value)
{
    public static ProductId Empty() => new(Guid.Empty);
    public static ProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}