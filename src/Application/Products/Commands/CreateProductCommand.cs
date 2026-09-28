using Application.Common.Interfaces.Repositories;
using Domain.Products;
using MediatR;

namespace Application.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public required string Title { get; init; }
}

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetByName(request.Title.Trim(), cancellationToken);
        if (existingProduct != null)
        {
            throw new ArgumentException($"Product with name {request.Title} already exists");
        }

        var product = Product.New(Guid.NewGuid(), request.Title);
        return await productRepository.Add(product, cancellationToken);
    }
}