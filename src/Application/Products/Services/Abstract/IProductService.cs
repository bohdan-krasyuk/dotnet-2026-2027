using Domain.Products;

namespace Application.Products.Services.Abstract;

public interface IProductService
{
    IReadOnlyList<Product> GetProducts();
    Product Add(string title);
}