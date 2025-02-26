using challenge.Core.Interfaces.Repositories;
using challenge.Core.Interfaces.Services;
using challenge.Services;
using challenge.Core.Dtos;
using Moq;
using Microsoft.Extensions.Logging;
using challenge.Core.Models;

namespace challenge.Tests.Services;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object, new Mock<ILogger<UserService>>().Object);
    }

    [Fact]
    public async Task GetUserByUsername_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var expectedUsername = "TestUser";
        var expectedUser = new User { Username = expectedUsername };
        _userRepositoryMock.Setup(repo => repo.GetUserAsync(expectedUsername))
                           .ReturnsAsync(expectedUser);

        // Act
        var result = await _userService.GetUserAsync(expectedUsername);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedUsername, result.Value.Username);
    }

    [Fact]
    public async Task GetUserByUsername_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetUserAsync(It.IsAny<string>()))
                           .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetUserAsync("NonExistentUser");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User not found.", result.Error);
    }
}
