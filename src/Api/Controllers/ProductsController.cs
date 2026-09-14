using Api.Dtos;
using Application.Products.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await productService.GetProducts(cancellationToken);

        return products.Select(x => new ProductDto(x.Id, x.Title)).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        [FromBody] CreateProductDto product,
        CancellationToken cancellationToken)
    {
        var newProduct = await productService.Add(product.Title, cancellationToken);

        return new ProductDto(newProduct.Id, newProduct.Title);
    }
}