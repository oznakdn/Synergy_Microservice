using Moq;
using Synergy.IdentityService.Application.Commands.UserCommands.RegisterUser;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.CommandTests;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> mockUserRepo;
    private readonly RegisterUserCommandHandler handler;

    public RegisterUserCommandHandlerTests()
    {
        mockUserRepo = new Mock<IUserRepository>();
        handler = new RegisterUserCommandHandler(mockUserRepo.Object);
    }

    [Fact]
    public async Task Handle_WithExistingUsername_ReturnsFailureResult()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Register = new Shared.Dtos.UserDtos.RegisterDto("existinguser", "test@test.com", "password123")
        };
        
        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(new User());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task Handle_WithNewUsername_ReturnsSuccessResult()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Register = new Shared.Dtos.UserDtos.RegisterDto("existinguser", "test@test.com", "password123")
        };

        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), It.IsAny<System.Threading.CancellationToken>()))!
            .ReturnsAsync((User)null!);

        var user = new User
        {
            Username = command.Register.Username,
            Email = command.Register.Email,
            Password = command.Register.Password
        };

        mockUserRepo.Setup(r => r.CreateAsync(It.Is<User>(u =>
            u.Username == user.Username &&
            u.Email == user.Email &&
            u.Password == user.Password),default!))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(204, result.StatusCode);
    }
}