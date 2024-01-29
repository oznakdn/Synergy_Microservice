using MediatR;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<TokenDto>>
{
    private readonly IUserRepository userRepo;
    private readonly ITokenGenerator tokenGenerator;

    public LoginUserQueryHandler(IUserRepository userRepo, ITokenGenerator tokenGenerator)
    {
        this.userRepo = userRepo;
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<Result<TokenDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetAsync(_ => _.Username == request.Login.Username && _.Password == request.Login.Password);

        if (user is null)
            return (Result<TokenDto>)Result<TokenDto>.Failure(400);


        var tokenDto = tokenGenerator.GenerateToken(user, user.Role ?? default);
        user.Token = tokenDto.RefreshToken;
        user.TokenExpire = DateTime.Now.AddDays(7);

        await userRepo.Update(user);

        return Result<TokenDto>.Success(200, tokenDto);

    }
}
