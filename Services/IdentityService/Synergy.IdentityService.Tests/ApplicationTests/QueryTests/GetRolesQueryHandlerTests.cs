using Moq;
using Synergy.IdentityService.Application.Queries.RoleQueries.GetRoles;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;

namespace Synergy.IdentityService.Tests.ApplicationTests.QueryTests;

public class GetRolesQueryHandlerTests
{
    private readonly Mock<IRoleRepository> mockRoleRepo;
    private readonly GetRolesQueryHandler queryHandler;

    public GetRolesQueryHandlerTests()
    {
        mockRoleRepo = new Mock<IRoleRepository>();
        queryHandler = new GetRolesQueryHandler(mockRoleRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenCalled_ReturnsRolesFromRepository()
    {
        // Arrange
        string userRoleId = Guid.NewGuid().ToString();
        string adminRoleId = Guid.NewGuid().ToString();
        var roles = new List<Role>
            {
                new Role { Id = adminRoleId, RoleName = "Admin", Description = "Administrator" },
                new Role { Id = userRoleId, RoleName = "User", Description = "Standard User" }
            };

        mockRoleRepo.Setup(r => r.GetAllAsync(null!, It.IsAny<CancellationToken>()))
            .ReturnsAsync(roles);

        // Act
        var result = await queryHandler.Handle(new GetRolesQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Values!.Count());
        Assert.Contains(result.Value!.Id, adminRoleId);
        Assert.Contains(result.Value.Id, userRoleId);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrows_ReturnsFailure()
    {
        // Arrange
        mockRoleRepo.Setup(r => r.GetAllAsync(null!, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new System.Exception("Database error"));

        // Act
        var result = await queryHandler.Handle(new GetRolesQuery(), CancellationToken.None);

        // Assert
        Assert.True(!result.IsSuccess);
    }
}
