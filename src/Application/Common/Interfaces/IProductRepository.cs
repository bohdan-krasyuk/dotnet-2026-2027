using Domain.Products;

namespace Application.Common.Interfaces;

public interface IProductRepository
{
    Product Add(Product product);
    Product Update(Product product);
    IReadOnlyList<Product> GetAll();
    Product? GetByTitle(string title);
}