using Microsoft.EntityFrameworkCore;
using Synergy.TeamService.Domain.Models;

namespace Synergy.TeamService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>opt): base(opt)
    {
        
    }

    public DbSet<Team>Teams { get; set; }
    public DbSet<Developer>Developers { get; set; }
    public DbSet<DeveloperSkill>DevelopersSkills { get; set;}
    public DbSet<Technology>Technologies { get; set; }
    public DbSet<Contact> Contacts { get; set; }

}
