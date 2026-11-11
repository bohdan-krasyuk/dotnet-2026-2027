using Domain.Products;

namespace Application.Common.Interfaces.Queries;

public interface IProductQueries
{
    Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> GetBySpecification(
        string specification,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> Filter(
        string? title,
        ProductStatus? status,
        DateTime? createdAtFrom,
        DateTime? createdAtTo,
        CancellationToken cancellationToken);
}