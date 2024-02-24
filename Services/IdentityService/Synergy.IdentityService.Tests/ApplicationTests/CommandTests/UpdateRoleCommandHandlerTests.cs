using Moq;
using Synergy.IdentityService.Application.Commands.RoleCommands.UpdateRole;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Tests.ApplicationTests.CommandTests;

public class UpdateRoleCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenRoleDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var mockRepo = new Mock<IRoleRepository>();
        mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), default))!
            .ReturnsAsync((Role)null!);

        var handler = new UpdateRoleCommandHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new UpdateRoleCommand(new UpdateRoleDto(Guid.NewGuid().ToString(), "Admin", "Allows: GET - POST")), default);

        // Assert
        Assert.IsType<Result>(result);
    }

    [Fact]
    public async Task Handle_WhenRoleExists_UpdatesRole()
    {
        // Arrange
        var role = new Role
        {
            Id = Guid.NewGuid().ToString(),
            RoleName = "Old Name",
            Description = "Old Description"
        };

        var mockRepo = new Mock<IRoleRepository>();
        mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), default))
            .ReturnsAsync(role);

        //mockRepo.Setup(r => r.GetAsync(x => x.Id == "", default)).ReturnsAsync(role);

        var handler = new UpdateRoleCommandHandler(mockRepo.Object);

        var command = new UpdateRoleCommand(new UpdateRoleDto(Guid.NewGuid().ToString(), "Admin", "Allows: GET - POST"));


        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.IsType<Result>(result);
        Assert.Equal("New Name", role.RoleName);
        Assert.Equal("New Description", role.Description);
    }
}
