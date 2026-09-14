using Application.Common.Interfaces;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product> Add(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Products.ToListAsync(cancellationToken);
    }
}