using Domain.Products;

namespace Api.Dtos;

public record ProductDto(Guid? Id, string Title, List<ProductSpecificationDto> Specification)
{
    public static ProductDto FromDomainModel(Product product)
        => new(
            Id: product.Id.Value,
            Title: product.Title,
            Specification: product.Specifications!.Select(ProductSpecificationDto.FromDomainModel).ToList());
}

public record ProductSpecificationDto(string Title, string Value)
{
    public static ProductSpecificationDto FromDomainModel(ProductSpecification productSpecification)
        => new(productSpecification.Title, productSpecification.Value);

    public static ProductSpecification ToDomainModel(ProductSpecificationDto productSpecification)
        => ProductSpecification.New(productSpecification.Title, productSpecification.Value);
}