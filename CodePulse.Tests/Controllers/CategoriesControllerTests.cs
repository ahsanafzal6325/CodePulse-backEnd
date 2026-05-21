using CodePulse.API.Controllers;
using CodePulse.Application.Categories;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodePulse.Tests.Controllers;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryAppService> _mockCategoryAppService;
    private readonly Mock<ILogger<CategoriesController>> _mockLogger;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _mockCategoryAppService = new Mock<ICategoryAppService>();
        _mockLogger = new Mock<ILogger<CategoriesController>>();
        _controller = new CategoriesController(_mockCategoryAppService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CreateCategory_ShouldReturnOkResult_WithCreatedCategory()
    {
        // Arrange
        var request = new CreateCategoryRequestDto { Name = "Tech", UrlHandle = "tech" };
        var category = new Category { Id = Guid.NewGuid(), Name = "Tech", UrlHandle = "tech" };
        _mockCategoryAppService.Setup(s => s.CreateAsync(request)).ReturnsAsync(category);

        // Act
        var result = await _controller.CreateCategory(request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(category);
    }

    [Fact]
    public async Task GetAllCategories_ShouldReturnOkResult_WithCategories()
    {
        // Arrange
        var request = new CategoryRequestDto();
        var categories = new List<CategoryDto>
        {
            new CategoryDto { Id = Guid.NewGuid(), Name = "Tech", UrlHandle = "tech" },
            new CategoryDto { Id = Guid.NewGuid(), Name = "Science", UrlHandle = "science" }
        };
        _mockCategoryAppService.Setup(s => s.GetAllAsync(request)).ReturnsAsync(categories);

        // Act
        var result = await _controller.GetAllCategories(request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(categories);
    }

    [Fact]
    public async Task GetCategoryById_WhenCategoryExists_ShouldReturnOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var category = new CategoryDto { Id = id, Name = "Tech", UrlHandle = "tech" };
        _mockCategoryAppService.Setup(s => s.GetById(id)).ReturnsAsync(category);

        // Act
        var result = await _controller.GetCategoryById(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(category);
    }

    [Fact]
    public async Task GetCategoryById_WhenCategoryDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockCategoryAppService.Setup(s => s.GetById(id)).ReturnsAsync((CategoryDto?)null);

        // Act
        var result = await _controller.GetCategoryById(id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateCategory_ShouldReturnOkResult_WithUpdatedCategory()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateCategoryRequestDto { Name = "Updated", UrlHandle = "updated" };
        var category = new Category { Id = id, Name = "Updated", UrlHandle = "updated" };
        _mockCategoryAppService.Setup(s => s.UpdateAsync(id, request)).ReturnsAsync(category);

        // Act
        var result = await _controller.UpdateCategory(id, request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(category);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryExists_ShouldReturnOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockCategoryAppService.Setup(s => s.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCategory(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be("Category Deleted successfully");
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockCategoryAppService.Setup(s => s.DeleteAsync(id)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteCategory(id);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().Be("Category not found");
    }

    [Fact]
    public async Task GetCategoriesCount_ShouldReturnOkResult_WithCount()
    {
        // Arrange
        _mockCategoryAppService.Setup(s => s.GetCategoriesCount()).ReturnsAsync(5);

        // Act
        var result = await _controller.GetCategoriesCount();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(5);
    }
}
