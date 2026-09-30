using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(
    ISender sender,
    IProductQueries productQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await productQueries.GetAll(cancellationToken);

        return products.Select(ProductDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        [FromBody] CreateProductDto product,
        CancellationToken cancellationToken)
    {
        var input = new CreateProductCommand
        {
            Title = product.Title,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProductDto>>(
            p => ProductDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }
}