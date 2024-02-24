using Moq;
using Synergy.IdentityService.Application.Commands.RoleCommands.AssignRole;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.CommandTests;

public class AssignRoleCommandHandlerTests
{
    private readonly Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
    private readonly Mock<IRoleRepository> mockRoleRepo = new Mock<IRoleRepository>();
    private readonly AssignRoleCommandHandler handler;

    public AssignRoleCommandHandlerTests()
    {
        handler = new AssignRoleCommandHandler(mockUserRepo.Object, mockRoleRepo.Object);
    }

    [Fact]
    public async Task Handle_WithInvalidUserId_ReturnsNotFoundResult()
    {
        // Arrange
        var command = new AssignRoleCommand { UserId = Guid.NewGuid().ToString(), RoleId = Guid.NewGuid().ToString() };
        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default))!.ReturnsAsync((User)null!);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(Result.Failure(404), result);
    }

    [Fact]
    public async Task Handle_WithInvalidRoleId_ReturnsNotFoundResult()
    {
        // Arrange
        var command = new AssignRoleCommand { UserId = Guid.NewGuid().ToString(), RoleId = "" };
        var user = CreateTestUser();
        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default)).ReturnsAsync(user);
        mockRoleRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), default))!.ReturnsAsync((Role)null!);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(Result.Failure(404), result);
    }

    [Fact]
    public async Task Handle_WithValidIds_UpdatesUserRole()
    {
        // Arrange
        var command = new AssignRoleCommand { UserId = Guid.NewGuid().ToString(), RoleId = Guid.NewGuid().ToString() };
        var user = CreateTestUser();
        var role = CreateTestRole();
        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default)).ReturnsAsync(user);
        mockRoleRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), default)).ReturnsAsync(role);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(Result.Success(204), result);
        Assert.Same(role, user.Role);
        mockUserRepo.Verify(r => r.Update(user, default!), Times.Once());
    }

    [Fact]
    public async Task Handle_WithUserAlreadyHavingRole_ReturnsBadRequest()
    {
        // Arrange
        var command = new AssignRoleCommand { UserId = Guid.NewGuid().ToString(), RoleId = Guid.NewGuid().ToString() };
        var user = CreateTestUser();
        user.Role = CreateTestRole();
        mockUserRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default)).ReturnsAsync(user);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(Result.Failure(400, error: "User already has this role!"), result);
    }

    private static User CreateTestUser()
    {
        string id = Guid.NewGuid().ToString();
        return new User { Id = id};
    }

    private static Role CreateTestRole()
    {
        string id = Guid.NewGuid().ToString();
        return new Role { Id = id, RoleName = "TestRole" };
    }
}

