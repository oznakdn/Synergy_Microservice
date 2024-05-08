using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class CommentRepo : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepo(AppDbContext db) : base(db)
    {
    }
}
