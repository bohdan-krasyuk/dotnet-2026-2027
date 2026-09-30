using Domain.Products;

namespace Api.Dtos;

public record ProductDto(Guid Id, string Title)
{
    public static ProductDto FromDomainModel(Product product)
        => new(product.Id.Value, product.Title);
}

public record CreateProductDto(string Title);