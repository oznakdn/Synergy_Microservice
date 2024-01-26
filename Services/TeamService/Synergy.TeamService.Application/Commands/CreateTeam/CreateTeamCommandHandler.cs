using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateTeamCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTeamCommandValidator();
        var validate = validator.Validate(request.CreateTeam);
        var erros = new List<string>();
        if (!validate.IsValid)
        {
            validate.Errors.ForEach(_ => erros.Add(_.ErrorMessage));
        }

        if (erros.Any())
            return Result.Failure(400, erros);

        var team = new Team
        {
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            TeamName = request.CreateTeam.TeamName,
            TeamDescription = request.CreateTeam.TeamDescription
        };

        _manager.Team.Insert(team);
        var result = await _manager.SaveAsync(cancellationToken);

        if(result>0)
            return Result.Success(204);

        return Result.Failure(500);
    }
}
