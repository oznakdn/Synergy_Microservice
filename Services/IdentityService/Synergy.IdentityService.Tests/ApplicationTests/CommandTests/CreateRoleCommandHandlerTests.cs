using Moq;
using Synergy.IdentityService.Application.Commands.RoleCommands.CreateRole;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.CommandTests;

public class CreateRoleCommandHandlerTests
{
    private readonly Mock<IRoleRepository> mockRoleRepo = new Mock<IRoleRepository>();
    private readonly CreateRoleCommandHandler handler;

    public CreateRoleCommandHandlerTests()
    {
        handler = new CreateRoleCommandHandler(mockRoleRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenRoleExists_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateRoleCommand
        {
            CreateRole = new Shared.Dtos.RoleDtos.CreateRoleDto("Admin", "Allows: GET - POST")
        };

        mockRoleRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), default!))
            .ReturnsAsync(new Domain.Models.Role());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WhenRoleDoesNotExist_CreatesRole()
    {
        // Arrange
        var command = new CreateRoleCommand
        {
            CreateRole = new Shared.Dtos.RoleDtos.CreateRoleDto("Admin", "Allows: GET - POST")
        };

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        mockRoleRepo
            .Verify(r => r.CreateAsync(It.Is<Role>(role => role.RoleName == command.CreateRole.RoleName), default), Times.Once);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WhenErrorOccurs_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateRoleCommand();

        mockRoleRepo.Setup(r => r.CreateAsync(It.IsAny<Role>(),default))
            .ThrowsAsync(new System.Exception());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
