using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Interfaces;

namespace Epias.Data.Interfaces;

public interface IRepository<T> where T : class, IEntity, new()
{
    Task<IList<T>> GetAllByDateAsync(Expression<Func<T, bool>> predicate = null);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<int> SaveChangesAsync();
    Task<T> Get(Expression<Func<T, bool>> predicate = null);
}
