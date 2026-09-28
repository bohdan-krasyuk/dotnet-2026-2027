using Domain.Products;

namespace Application.Common.Interfaces.Queries;

public interface IProductQueries
{
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
}