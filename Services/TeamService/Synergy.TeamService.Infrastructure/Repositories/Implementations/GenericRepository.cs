using Microsoft.EntityFrameworkCore;
using Synergy.TeamService.Domain.Models.Abstracts;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class, IEntity
{
    protected readonly AppDbContext _db;
    public GenericRepository(AppDbContext db)
    {
        _db = db;
    }
    public void Insert(T entity) => _db.Entry(entity).State = EntityState.Added;
    public void Update(T entity) => _db.Entry(entity).State = EntityState.Modified;
    public void Delete(T entity) => _db.Entry(entity).State = EntityState.Deleted;

    public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _db.Set<T>().AsQueryable();
        query = filter is null ? query : query.Where(filter);

        if (includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await Task.FromResult(query);
    }


}
