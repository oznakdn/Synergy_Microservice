using MediatR;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserByRefreshToken;

public class GetUserByRefreshTokenQuery : IRequest<IResult<TokenDto>>
{
    public GetUserByRefreshTokenQuery(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public string RefreshToken { get; set; }    
}
