using Domain.Products;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
    Task<Option<Product>> GetByName(string name, CancellationToken cancellationToken);
    Task<Option<Product>> GetById(ProductId id, CancellationToken cancellationToken);
    Task<Product> Add(Product product, CancellationToken cancellationToken);
    Task<Product> Update(Product product, CancellationToken cancellationToken);
}