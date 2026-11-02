namespace Domain.Categories;

public record CategoryId(Guid Value)
{
    public static CategoryId Empty() => new(Guid.Empty);
    public static CategoryId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}