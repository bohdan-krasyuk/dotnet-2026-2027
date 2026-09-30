using Domain.Products;

namespace Application.Products.Exceptions;

public abstract class ProductException(ProductId productId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProductId ProductId { get; } = productId;
}

public class ProductAlreadyExistsException(ProductId productId) : ProductException(productId, $"Product already exists under id {productId}");

public class ProductNotFoundException(ProductId productId) : ProductException(productId, $"Product not found under id {productId}");

public class ProductUnhandledException(ProductId productId, Exception? innerException = null)
    : ProductException(productId, $"Unhandled exception under id {productId}", innerException);