using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository, IProductQueries
{
    public async Task<Product?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Title == name, cancellationToken);
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
}