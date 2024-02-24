using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.GetUserById;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _handler = new GetUserByIdQueryHandler(_mockUserRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        _mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))!
                      .ReturnsAsync((User)null!);

        var query = new GetUserByIdQuery(userId: Guid.NewGuid().ToString());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<UserDto?>(result.Value);
    }

    [Fact]
    public async Task Handle_WhenUserFound_ReturnsUserDto()
    {
        // Arrange 
        string userId = Guid.NewGuid().ToString();
        var testUser = new User { Id = userId, Username = "test", Email = "test@test.com" };
        _mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(testUser);

        var query = new GetUserByIdQuery(userId: userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<UserDto>(result);
        Assert.Equal("test", result.Value!.Username);
    }

    [Fact]
    public async Task Handle_WithUserRole_PopulatesRoleName()
    {
        // Arrange
        string userId = Guid.NewGuid().ToString();
        var testUser = new User
        {
            Id = userId,
            Username = "test",
            Email = "test@test.com",
            Role = new Role { RoleName = "Admin" }
        };
        _mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(testUser);

        var query = new GetUserByIdQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal("Admin", result.Value!.Role);
    }
}
