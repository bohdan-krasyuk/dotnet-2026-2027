using Domain.Products;

namespace Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product> Add(Product product, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
}