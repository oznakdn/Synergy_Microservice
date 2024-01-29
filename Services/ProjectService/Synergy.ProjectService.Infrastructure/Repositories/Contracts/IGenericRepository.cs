using Synergy.ProjectService.Domain.Models.Abstracts;
using System.Linq.Expressions;

namespace Synergy.ProjectService.Infrastructure.Repositories.Contracts;

public interface IGenericRepository<T>
    where T : class,IEntity
{
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);

}
