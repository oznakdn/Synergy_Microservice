﻿using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.TechnologyDtos;

namespace Synergy.TeamService.Application.Queries.GetTechnologies;


public class GetTechnologiesQueryHandler : IRequestHandler<GetTechnologiesQuery, Result<TechnologyDto>>
{
    private readonly IRepositoryManager _manager;

    public GetTechnologiesQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<TechnologyDto>> Handle(GetTechnologiesQuery request, CancellationToken cancellationToken)
    {
        var technologies = await _manager.Technology.GetTechnologies();
      
        var technologyDto = technologies.Select(_ => new TechnologyDto(_.Id.ToString(), _.Name, _.Description)).ToList();
        return Result<TechnologyDto>.Success(statusCode: 200, values: technologyDto);
    }
}
