using MediatR;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.UserCommands.RegisterUser;

public class RegisterUserCommand : IRequest<Result>
{
    public RegisterDto Register { get; set; }
}
