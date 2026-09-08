using Application.Common.Interfaces;
using Application.Products.Services.Abstract;
using Domain.Products;

namespace Application.Products.Services.Implementation;

public class ProductService(IProductRepository productRepository): IProductService
{
    public IReadOnlyList<Product> GetProducts()
    {
        return productRepository.GetAll();
    }

    public Product Add(string title)
    {
        var existingProduct = productRepository.GetByTitle(title);
        if (existingProduct != null)
        {
            throw new ArgumentException($"Product with {title} already exists");
        }

        var entity = Product.New(Guid.NewGuid(), title);
        return productRepository.Add(entity);
    }
}