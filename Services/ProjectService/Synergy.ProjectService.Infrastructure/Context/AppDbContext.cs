using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Domain.Models;

namespace Synergy.ProjectService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Comment> Comments { get; set; }

}
