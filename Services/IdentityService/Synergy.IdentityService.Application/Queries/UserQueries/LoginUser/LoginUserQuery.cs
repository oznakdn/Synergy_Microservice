using MediatR;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.LoginUser;

public class LoginUserQuery : IRequest<Result<TokenDto>>
{
    public LoginDto Login { get; set; }

}
