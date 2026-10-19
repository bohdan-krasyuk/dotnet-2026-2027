using System.Net.Http.Json;
using Api.Dtos;
using Domain.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Api.Tests.Integration.Products;

public class ProductsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    public ProductsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldCreateProduct()
    {
        // Arrange
        var request = new CreateProductDto("Test product");

        // Act
        var response = await Client.PostAsJsonAsync("products", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<ProductDto>();
        var dbProduct = await Context.Products.FirstOrDefaultAsync(x => x.Id.Equals(new ProductId(responseDto.Id)));

        dbProduct.Title.Should().Be(request.Title);
    }

    public async Task InitializeAsync()
    {
        // seed data
    }

    public async Task DisposeAsync()
    {
        // clean-up data
    }
}