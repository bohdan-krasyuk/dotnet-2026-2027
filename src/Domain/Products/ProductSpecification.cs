namespace Domain.Products;

public class ProductSpecification
{
    public string Title { get; set; }
    public string Value { get; set; }

    // public SubSpecification? SubSpecification { get; set; } = null;

    private ProductSpecification(string title, string value)
    {
        Title = title;
        Value = value;
    }

    public static ProductSpecification New(string title, string value)
        => new(title, value);
}

/*public class SubSpecification
{
    public string OrderIndex { get; set; }

    private SubSpecification(string orderIndex)
    {
        OrderIndex = orderIndex;
    }

    public static SubSpecification New(string orderIndex)
        => new(orderIndex);
}*/