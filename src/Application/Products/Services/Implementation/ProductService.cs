using Application.Common.Interfaces;
using Application.Products.Services.Abstract;
using Domain.Products;

namespace Application.Products.Services.Implementation;

public class ProductService(IProductRepository productRepository): IProductService
{
    public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken)
    {
        return await productRepository.GetAll(cancellationToken);
    }

    public async Task<Product> Add(string title, CancellationToken cancellationToken)
    {
        var product = Product.New(Guid.NewGuid(), title);
        return await productRepository.Add(product, cancellationToken);
    }
}