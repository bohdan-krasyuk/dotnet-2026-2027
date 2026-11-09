using Domain.Common;

namespace Domain.Products;

public static class ProductStatusValues
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}

public class ProductStatus : ValueObject
{
    private string Value { get; }

    private ProductStatus(string status)
    {
        Value = status;
    }

    public static ProductStatus From(string value)
    {
        var status = new ProductStatus(value);

        if (!AllowedStatuses.Contains(status))
        {
            throw new ArgumentException($"Invalid status: {status}");
        }

        return status;
    }

    public static ProductStatus Active => new(ProductStatusValues.Active);
    public static ProductStatus Inactive => new(ProductStatusValues.Inactive);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static IEnumerable<ProductStatus> AllowedStatuses
    {
        get
        {
            yield return Active;
            yield return Inactive;
        }
    }
}