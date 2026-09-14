using Domain.Products;

namespace Application.Products.Services.Abstract;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken);
    Task<Product> Add(string title, CancellationToken cancellationToken);
}