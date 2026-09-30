using Application.Common.Interfaces.Repositories;
using Application.Products.Exceptions;
using Domain.Products;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Products.Commands;

public record UpdateProductCommand : IRequest<Either<ProductException, Product>>
{
    public required Guid ProductId { get; init; }
    public required string Name { get; init; }
}

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, Either<ProductException, Product>>
{
    public async Task<Either<ProductException, Product>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);

        var product = await productRepository.GetById(productId, cancellationToken);
        return await product.MatchAsync(
            p => CheckDuplicates(p.Id, request.Name, cancellationToken)
                .BindAsync(_ => UpdateEntity(p, request, cancellationToken)),
            () => new ProductNotFoundException(productId));
    }

    private async Task<Either<ProductException, Product>> UpdateEntity(
        Product product,
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            product.UpdateName(request.Name);

            return await productRepository.Update(product, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProductUnhandledException(product.Id, exception);
        }
    }

    private async Task<Either<ProductException, Unit>> CheckDuplicates(
        ProductId currentProductId,
        string name,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByName(name, cancellationToken);

        return product.Match<Either<ProductException, Unit>>(
            p => p.Id == currentProductId ? Unit.Default : new ProductAlreadyExistsException(p.Id),
            () => Unit.Default);
    }
}