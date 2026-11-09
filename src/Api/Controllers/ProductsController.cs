using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Products.Commands;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(
    ISender sender,
    IProductQueries productQueries) : ControllerBase
{
    [HttpGet("specifications/{title}")]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetBySpecification(
        [FromRoute] string title,
        CancellationToken cancellationToken)
    {
        var products = await productQueries.GetBySpecification(title, cancellationToken);

        return products.Select(ProductDto.FromDomainModel).ToList();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await productQueries.GetAll(cancellationToken);

        return products.Select(ProductDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        [FromBody] ProductDto product,
        CancellationToken cancellationToken)
    {
        var input = new CreateProductCommand
        {
            Title = product.Title,
            Specifications = product.Specification.Select(ProductSpecificationDto.ToDomainModel).ToList()
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProductDto>>(
            p => ProductDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpPut("{productId:guid}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(
        [FromRoute] Guid productId,
        [FromBody] ProductDto product,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProductCommand
        {
            ProductId = productId,
            Title = product.Title
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProductDto>>(
            p => ProductDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpPost("products/{productId:guid}/image")]
    public async Task<ActionResult> UploadImage(
        [FromBody] IFormFile imageFile,
        CancellationToken cancellationToken)
    {
        var input = new UploadProductImageCommand
        {
            FileName = imageFile.Name,
            FileStream = imageFile.OpenReadStream()
        };

        return Ok();
    }
}