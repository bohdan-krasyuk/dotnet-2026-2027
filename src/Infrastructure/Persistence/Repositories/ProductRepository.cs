using Application.Common.Interfaces;
using Domain.Products;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private IList<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>();
    }

    public Product Add(Product product)
    {
        _products.Add(product);
        return product;
    }

    public Product Update(Product product)
    {
        var entity = _products.First(x => x.Id == product.Id);

        var index = _products.IndexOf(entity);
        _products[index] = product;

        return product;
    }

    public IReadOnlyList<Product> GetAll()
    {
        return _products.ToList();
    }

    public Product? GetByTitle(string title)
    {
        return _products.FirstOrDefault(x => x.Title == title);
    }
}