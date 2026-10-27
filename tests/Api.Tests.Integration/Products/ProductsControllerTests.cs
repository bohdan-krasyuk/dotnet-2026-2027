using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data.Products;
using Xunit;

namespace Api.Tests.Integration.Products;

public class ProductsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Product _firstTestProduct;
    private readonly Product _secondTestProduct;
    private readonly Product _thirdTestProduct;

    private readonly string _createProductRoute;
    private readonly string _updateProductRoute;

    public ProductsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _firstTestProduct = ProductsTestData.FirstTestProduct();
        _secondTestProduct = ProductsTestData.SecondTestProduct();
        _thirdTestProduct = ProductsTestData.ThirdTestProduct();

        _createProductRoute = "products";
        _updateProductRoute = $"products/{_secondTestProduct.Id}";
    }

    [Fact]
    public async Task ShouldCreateProduct()
    {
        // Arrange
        var request = new ProductDto(
            Id: null,
            Title: _firstTestProduct.Title);

        // Act
        var response = await Client.PostAsJsonAsync(_createProductRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<ProductDto>();
        responseDto.Should().NotBeNull();
        responseDto.Id.Should().NotBeNull();

        var dbProduct = await Context.Products.FirstOrDefaultAsync(x => x.Id.Equals(new ProductId(responseDto.Id.Value)));

        dbProduct.Should().NotBeNull();
        dbProduct.Title.Should().Be(_firstTestProduct.Title);
    }

    [Fact]
    public async Task ShouldNotCreateProductBecauseTitleAlreadyTaken()
    {
        // Arrange
        var request = new ProductDto(
            Id: null,
            Title: _secondTestProduct.Title);

        // Act
        var response = await Client.PostAsJsonAsync(_createProductRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        Context.Products.Single(x => x.Title == _secondTestProduct.Title).Id.Should().Be(_secondTestProduct.Id);
    }

    [Fact]
    public async Task ShouldUpdateProduct()
    {
        // Arrange
        var request = new ProductDto(
            Id: _secondTestProduct.Id.Value,
            Title: _thirdTestProduct.Title);

        // Act
        var response = await Client.PutAsJsonAsync(_updateProductRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<ProductDto>();
        responseDto.Should().NotBeNull();
        responseDto.Id.Should().NotBeNull();

        var dbProduct = await Context.Products.FirstOrDefaultAsync(x => x.Id.Equals(new ProductId(responseDto.Id.Value)));

        dbProduct.Should().NotBeNull();
        dbProduct.Title.Should().Be(_thirdTestProduct.Title);
    }

    public async Task InitializeAsync()
    {
        await Context.Products.AddAsync(_secondTestProduct);

        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.Products.RemoveRange(Context.Products);

        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}