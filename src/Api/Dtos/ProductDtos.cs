namespace Api.Dtos;

public record ProductDto(Guid Id, string Title);

public record CreateProductDto(string Title);