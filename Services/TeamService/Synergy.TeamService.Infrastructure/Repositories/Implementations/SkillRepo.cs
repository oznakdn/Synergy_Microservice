﻿using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class SkillRepo : GenericRepository<Skill>, ISkillRepo
{
    public SkillRepo(AppDbContext db) : base(db)
    {
    }
}
