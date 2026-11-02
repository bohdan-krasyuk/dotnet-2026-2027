using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductCategoryRepository(ApplicationDbContext context)
{
    public async Task<IReadOnlyList<ProductCategory>> GetAllWithProducts(CancellationToken cancellationToken)
    {
        return await context.ProductCategories.Include(x => x.Product).ToListAsync(cancellationToken);
    }
}