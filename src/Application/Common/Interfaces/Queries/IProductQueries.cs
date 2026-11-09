using Domain.Products;

namespace Application.Common.Interfaces.Queries;

public interface IProductQueries
{
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> GetBySpecification(
        string specification,
        CancellationToken cancellationToken);
}