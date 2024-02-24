using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.GetUserByRefreshToken;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class GetUserByRefreshTokenQueryHandlerTests
{
    private readonly GetUserByRefreshTokenQueryHandler sut;
    private readonly Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
    private readonly Mock<ITokenGenerator> tokenGeneratorMock = new Mock<ITokenGenerator>();

    public GetUserByRefreshTokenQueryHandlerTests()
    {
        sut = new GetUserByRefreshTokenQueryHandler(userRepoMock.Object, tokenGeneratorMock.Object);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ReturnsFailureResult()
    {
        // Arrange
        var query = new GetUserByRefreshTokenQuery("token");
        userRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))
            .ReturnsAsync((User)null!);

        // Act
        var result = await sut.Handle(query, default);

        // Assert
        Assert.IsType<Result<TokenDto>>(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal("User not found!", result.Message);
    }

    [Fact]
    public async Task Handle_WhenUserFound_ReturnsSuccessResultWithToken()
    {
        // Arrange
        var query = new GetUserByRefreshTokenQuery("token");
        var user = new User();

        var expectedToken = new TokenDto("",new DateTimeOffset(),"",new DateTimeOffset(),new UserDto(user.Id,user.Username,user.Email,user.MemberId));

        userRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))
            .ReturnsAsync(user);
        tokenGeneratorMock.Setup(x => x.GenerateToken(user)).Returns(expectedToken);

        // Act
        var result = await sut.Handle(query, default);

        // Assert
        Assert.IsType<Result<TokenDto>>(result);
        Assert.True(result.IsSuccess);
        Assert.Same(expectedToken, result.Value);
        userRepoMock.Verify(x => x.Update(user,default!), Times.Once());
    }
}
