﻿using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.AddDeveloperSkill;

public class AddDeveloperSkillCommandHandler : IRequestHandler<AddDeveloperSkillCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public AddDeveloperSkillCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(AddDeveloperSkillCommand request, CancellationToken cancellationToken)
    {
        var developer = await _manager.Developer.GetAsync(_ => _.Id == Guid.Parse(request.AddDeveloperSkill.DeveloperId));
        if (!developer.Any())
            return Result.Failure(404, "Developer not found!");

        var technology = await _manager.Technology.GetAsync(_ => _.Id == Guid.Parse(request.AddDeveloperSkill.TechnologyId));
        if(!technology.Any())
            return Result.Failure(404, "Technology not found!");

        var developerSkill = new DeveloperSkill
        {
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            DeveloperId = developer.SingleOrDefault()!.Id,
            TechnologyId = technology.SingleOrDefault()!.Id,
            Experience = request.AddDeveloperSkill.Experience
        };

        _manager.DeveloperSkill.Insert(developerSkill);
        await _manager.SaveAsync(cancellationToken);

        return Result.Success(204);

    }
}
