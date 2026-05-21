using CodePulse.API.Controllers;
using CodePulse.Application.Products;
using CodePulse.Application.Products.Dto;
using CodePulse.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodePulse.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductAppService> _mockProductAppService;
    private readonly Mock<ILogger<ProductController>> _mockLogger;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductAppService = new Mock<IProductAppService>();
        _mockLogger = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_mockProductAppService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CreateCategory_ShouldReturnOkResult_WithCreatedProduct()
    {
        // Arrange
        var request = new ProductDto { Name = "Laptop", Price = 999.99m };
        var product = new Product { Id = 1, Name = "Laptop", Price = 999.99m };
        _mockProductAppService.Setup(s => s.CreateAsync(request)).ReturnsAsync(product);

        // Act
        var result = await _controller.CreateCategory(request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(product);
    }

    [Fact]
    public async Task CreateCategory_ShouldCallServiceOnce()
    {
        // Arrange
        var request = new ProductDto { Name = "Phone", Price = 499.99m };
        var product = new Product { Id = 2, Name = "Phone", Price = 499.99m };
        _mockProductAppService.Setup(s => s.CreateAsync(request)).ReturnsAsync(product);

        // Act
        await _controller.CreateCategory(request);

        // Assert
        _mockProductAppService.Verify(s => s.CreateAsync(request), Times.Once);
    }
}
