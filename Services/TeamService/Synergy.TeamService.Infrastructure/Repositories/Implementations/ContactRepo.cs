using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class ContactRepo : GenericRepository<Contact>, IContactRepo
{
    public ContactRepo(AppDbContext db) : base(db)
    {
    }
}
