using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.GetUserByMemberId;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class GetUserByMemberIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
    private readonly GetUserByMemberIdQueryHandler sut;

    public GetUserByMemberIdQueryHandlerTests()
    {
        sut = new GetUserByMemberIdQueryHandler(mockUserRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenUserExists_ReturnsUserDto()
    {
        // Arrange
        var memberId = "123";
        var user = CreateTestUser(memberId);
        mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))
            .ReturnsAsync(user);

        // Act
        var result = await sut.Handle(new GetUserByMemberIdQuery(memberId), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(memberId, result.Value!.MemberId);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var memberId = "123";
        mockUserRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default!))!
            .ReturnsAsync((User)null!);

        // Act
        var result = await sut.Handle(new GetUserByMemberIdQuery(memberId: memberId), CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.StatusCode);
    }

    private User CreateTestUser(string memberId)
    {
        return new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "test",
            Email = "test@test.com",
            MemberId = memberId,
            Role = new Role { RoleName = "Admin" }
        };
    }
}

