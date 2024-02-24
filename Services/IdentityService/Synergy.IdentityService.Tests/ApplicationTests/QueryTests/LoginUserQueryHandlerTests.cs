using Microsoft.AspNetCore.Identity;
using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.LoginUser;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class LoginUserQueryHandlerTests
{
    private readonly Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
    private readonly Mock<ITokenGenerator> mockTokenGenerator = new Mock<ITokenGenerator>();
    private readonly LoginUserQueryHandler queryHandler;

    public LoginUserQueryHandlerTests()
    {
        queryHandler = new LoginUserQueryHandler(mockUserRepo.Object, mockTokenGenerator.Object);
    }

    [Fact]
    public async Task Handle_WithInvalidCredentials_ReturnsFailureResult()
    {
        // Arrange
        var query = new LoginUserQuery
        {
            Login = new LoginDto("invalid", "invalid")

        };

        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))!
            .ReturnsAsync((User)null!);

        // Act
        var result = await queryHandler.Handle(query, default);

        // Assert
        Assert.True(!result.IsSuccess);
        Assert.Equal(400, result.StatusCode);
    }

}
