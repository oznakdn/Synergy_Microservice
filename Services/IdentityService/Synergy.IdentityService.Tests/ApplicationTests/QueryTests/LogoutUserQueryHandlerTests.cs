using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.LogoutUser;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class LogoutUserQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly LogoutUserQueryHandler _handler;

    public LogoutUserQueryHandlerTests()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _handler = new LogoutUserQueryHandler(_mockUserRepo.Object);
    }

    [Fact]
    public async Task Handle_WithValidRefreshToken_ReturnsSuccess()
    {
        // Arrange
        var refreshToken = "valid_token";
        var user = new User { Token = refreshToken };
        _mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))
            .ReturnsAsync(user);

        var query = new LogoutUserQuery(refreshToken);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Handle_WithInvalidRefreshToken_ReturnsFailure()
    {
        // Arrange
        var refreshToken = "invalid_token";
        User user = null;
        _mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))!
            .ReturnsAsync(user);

        var query = new LogoutUserQuery(refreshToken);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(!result.IsSuccess);
        Assert.Equal(404, result.StatusCode);
    }
}