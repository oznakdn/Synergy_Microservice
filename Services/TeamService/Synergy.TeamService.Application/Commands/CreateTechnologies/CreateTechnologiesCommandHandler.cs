using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.CreateTechnologies;

public class CreateTechnologiesCommandHandler : IRequestHandler<CreateTechnologiesCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateTechnologiesCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateTechnologiesCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.CreateTechnologies!)
        {
            var query = await _manager.Technology.GetAsync(_ => _.Name == item.Name);
            
            if(query.Any())
            {
                return Result.Failure(400,$"{item.Name} is already exist!");
            }

            _manager.Technology.Insert(new Technology
            {
                Name = item.Name,
                Description = item.Description
            });

            await _manager.SaveAsync(cancellationToken);
        }

        return Result.Success(204);

    }
}
