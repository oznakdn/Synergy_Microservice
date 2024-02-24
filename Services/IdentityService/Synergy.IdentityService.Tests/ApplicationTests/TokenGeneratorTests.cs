using Moq;
using Synergy.IdentityService.Application.TokenService.Options;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Domain.Models;

namespace Synergy.IdentityService.Tests.ApplicationTests;

public class TokenGeneratorTests
{
    private readonly Mock<ITokenOption> _mockTokenOptions;
    private readonly TokenGenerator _tokenGenerator;

    public TokenGeneratorTests()
    {
        _mockTokenOptions = new Mock<ITokenOption>();
        _tokenGenerator = new TokenGenerator(_mockTokenOptions.Object);
    }

    [Fact]
    public void GenerateToken_WithValidUser_ReturnsTokenDto()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "test",
            Email = "test@example.com",
            MemberId = Guid.NewGuid().ToString(),
            Password = "test",
            Role = new Role
            {
                Id = Guid.NewGuid().ToString(),
                RoleName = "admin",
                Description = "Allows: GET"
            }
        };

       
        _mockTokenOptions.Setup(x => x.Key).Returns("1f7758cf-0bc9-406b-b761-0b2e523548b0bff79e57-ee57-4d2c-a6ef-4a624f33cef6");
        _mockTokenOptions.Setup(x => x.Issuer).Returns("http://localhost:5024");
        _mockTokenOptions.Setup(x => x.Audience).Returns("http://localhost:5024");

        // Act
        var result = _tokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
        Assert.NotNull(result.RefreshToken);
        Assert.NotNull(result.User);

        Assert.Equal(user.Id, result.User.Id);
        Assert.Equal(user.Username, result.User.Username);
        Assert.Equal(user.Email, result.User.Email);
    }

    [Fact]
    public void GenerateToken_WithRole_AddsRoleClaim()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "test",
            Email = "test@example.com",
            MemberId = Guid.NewGuid().ToString(),
            Password = "test",
            Role = new Role { Id = Guid.NewGuid().ToString(),RoleName = "admin"}
        };

        _mockTokenOptions.Setup(x => x.Key).Returns("1f7758cf-0bc9-406b-b761-0b2e523548b0bff79e57-ee57-4d2c-a6ef-4a624f33cef6");

        // Act
        var result = _tokenGenerator.GenerateToken(user);

        // Assert
        Assert.Contains(result.User.Role,"admin");
    }

    [Fact]
    public void GenerateToken_WithoutRole_OmitsRoleClaim()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "test",
            Email = "test@example.com",
            MemberId = Guid.NewGuid().ToString(),
            Password = "test"
        };

        _mockTokenOptions.Setup(x => x.Key).Returns("1f7758cf-0bc9-406b-b761-0b2e523548b0bff79e57-ee57-4d2c-a6ef-4a624f33cef6");

        // Act
        var result = _tokenGenerator.GenerateToken(user);

        // Assert
        Assert.DoesNotContain(result.Token, "role");
    }
}

