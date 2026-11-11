using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Products;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository, IProductQueries
{
    public async Task<IReadOnlyList<Product>> Filter(
        string? title,
        ProductStatus? status,
        DateTime? createdAtFrom,
        DateTime? createdAtTo,
        CancellationToken cancellationToken)
    {
        var query = context.Products.AsQueryable();

        if (string.IsNullOrEmpty(title))
        {
            query = query.Where(x => x.Title.ToLower().StartsWith(title.ToLower()));
        }

        if (status != null)
        {
            query = query.Where(x => x.Status == status);
        }

        if (createdAtFrom.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= createdAtFrom);
        }

        if (createdAtTo.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= createdAtTo);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Option<Product>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .FirstOrDefaultAsync(p => p.Title == name, cancellationToken);

        return entity ?? Option<Product>.None;
    }

    public async Task<Option<Product>> GetById(ProductId id, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return entity ?? Option<Product>.None;
    }

    public async Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Products.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetBySpecification(
        string specification,
        CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(x => x.Specifications!.Any(s => s.Title == specification))
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> Add(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> Update(Product product, CancellationToken cancellationToken)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);

        return product;
    }
}