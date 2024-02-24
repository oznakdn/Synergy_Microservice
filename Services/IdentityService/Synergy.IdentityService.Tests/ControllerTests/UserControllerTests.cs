using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Synergy.IdentityService.Api.Controllers;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Tests.ControllerTests;

public class UserControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _controller = new UserController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Login_WithValidLogin_ReturnsOkResult()
    {
        // Arrange
        var loginDto = new LoginDto("testUser", "testpassword");
        var loginResult = Result<LoginDto>.Failure(200);

        _mediatorMock.Setup(x => x.Send(loginDto, default))
                     .ReturnsAsync(loginResult);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(loginResult.StatusCode, okResult.StatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidLogin_ReturnsBadRequest()
    {
        // Arrange
        var loginDto = new LoginDto("testUser", "testpassword");
        var loginResult = Result<LoginDto>.Failure(400);

        _mediatorMock.Setup(x => x.Send(loginDto, default))
                     .ReturnsAsync(loginResult);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Relogin_WithValidToken_ReturnsOkResult()
    {
        // Arrange
        var refreshToken = "token";
        var userResult = Result<LoginDto>.Success(200);
        _mediatorMock.Setup(x => x.Send(refreshToken, default))
                     .ReturnsAsync(userResult);

        // Act
        var result = await _controller.Relogin(refreshToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(userResult.StatusCode, okResult.StatusCode);
    }

    [Fact]
    public async Task Relogin_WithInvalidToken_ReturnsNotFound()
    {
        // Arrange
        var refreshToken = "token";
        var userResult = Result<LoginDto>.Success(200);
        _mediatorMock.Setup(x => x.Send(refreshToken, default))
                     .ReturnsAsync(userResult);

        // Act
        var result = await _controller.Relogin(refreshToken);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

  
}
