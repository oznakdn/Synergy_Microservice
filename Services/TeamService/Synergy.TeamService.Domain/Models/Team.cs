﻿using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Team : AuditableEntity
{
    public string TeamName { get; set; }
    public string TeamDescription { get; set; }
    public ICollection<Member>Members { get; set; } = new HashSet<Member>();
}
