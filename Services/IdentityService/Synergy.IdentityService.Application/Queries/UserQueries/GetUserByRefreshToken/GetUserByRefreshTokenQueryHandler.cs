using MediatR;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserByRefreshToken;

public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, IResult<TokenDto>>
{
    private readonly IUserRepository userRepo;
    private readonly ITokenGenerator tokenGenerator;

    public GetUserByRefreshTokenQueryHandler(IUserRepository userRepo, ITokenGenerator tokenGenerator)
    {
        this.userRepo = userRepo;
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<IResult<TokenDto>> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetAsync(predicate: _ => _.Token == request.RefreshToken);
        if (user is null)
            return Result<TokenDto>.Failure(404, "User not found!");

        var token = tokenGenerator.GenerateToken(user);
        user.Token = token.RefreshToken;
        user.TokenExpire = Convert.ToDateTime(token.RefreshExpire);

        await userRepo.Update(user);
        return Result<TokenDto>.Success(token);

    }
}
