using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Products;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository, IProductQueries
{
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