using Microsoft.EntityFrameworkCore;
using Synergy.TeamService.Domain.Models;

namespace Synergy.TeamService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>opt): base(opt)
    {
        
    }

    public DbSet<Team>Teams { get; set; }
    public DbSet<Member>Members { get; set; }
    public DbSet<Skill>Skills { get; set;}
    public DbSet<Technology>Technologies { get; set; }
    public DbSet<Contact> Contacts { get; set; }

}
