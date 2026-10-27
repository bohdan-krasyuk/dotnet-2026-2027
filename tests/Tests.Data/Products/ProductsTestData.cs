using Domain.Products;

namespace Tests.Data.Products;

public static class ProductsTestData
{
    public static Product FirstTestProduct() => Product.New(ProductId.New(), "First test product");
    public static Product SecondTestProduct() => Product.New(ProductId.New(), "Second test product");
    public static Product ThirdTestProduct() => Product.New(ProductId.New(), "Third test product");
}