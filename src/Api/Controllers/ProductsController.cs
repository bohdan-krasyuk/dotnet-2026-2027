using Api.Dtos;
using Application.Products.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<ProductDto>> GetProducts()
    {
        var products = productService.GetProducts();

        return products.Select(x => new ProductDto(x.Id, x.Title)).ToList();
    }

    [HttpPost]
    public ActionResult<ProductDto> CreateProduct([FromBody] CreateProductDto product)
    {
        var newProduct = productService.Add(product.Title);

        return new ProductDto(newProduct.Id, newProduct.Title);
    }
}