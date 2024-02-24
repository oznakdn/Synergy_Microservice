using Moq;
using Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class GetUsersQueryHandlerTests
{
    private readonly Mock<IUserRepository> mockUserRepo;
    private readonly GetUsersQueryHandler queryHandler;

    public GetUsersQueryHandlerTests()
    {
        mockUserRepo = new Mock<IUserRepository>();
        queryHandler = new GetUsersQueryHandler(mockUserRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenCalled_ReturnsAllUsers()
    {
        // Arrange

        List<User> users = new List<User>
        {
            new User{Id = Guid.NewGuid().ToString(),Email = "user1@test.com",Username ="user1",MemberId=Guid.NewGuid().ToString(),Password = "123456"},
            new User{Id = Guid.NewGuid().ToString(),Email = "user2@test.com",Username ="user2",MemberId=Guid.NewGuid().ToString(),Password = "123456"}

        };

        List<UserDto> usersDto = users.Select(x => new UserDto(x.Id, x.Username, x.Email, x.MemberId, null)).ToList();
       
        mockUserRepo.Setup(x => x.GetAllAsync(default!, default))
            .ReturnsAsync(users);


        // Act
        var result = await queryHandler.Handle(
            new GetUsersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(usersDto, result.Values!);
    }

    [Fact]
    public async Task Handle_WhenRepoReturnsNull_ReturnsEmptyList()
    {


        // Arrange

        mockUserRepo.Setup(repo => repo.GetAllAsync(default!, default))
            .ReturnsAsync((List<User>)null!);

        // Act
        var result = await queryHandler.Handle(
            new GetUsersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Values!);
    }

    [Fact]
    public async Task Handle_WhenRepoThrows_ReturnsFailure()
    {
        // Arrange
        mockUserRepo.Setup(repo => repo.GetAllAsync(default!,default))
            .ThrowsAsync(new System.Exception("Error!"));

        // Act
        var result = await queryHandler.Handle(
            new GetUsersQuery(), CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
