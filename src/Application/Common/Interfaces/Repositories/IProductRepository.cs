using Domain.Products;

namespace Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
    Task<Product?> GetByName(string name, CancellationToken cancellationToken);
    Task<Product> Add(Product product, CancellationToken cancellationToken);
}