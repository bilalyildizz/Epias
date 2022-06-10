using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epias.Data.Concrete.EntityFramework.Contexts;
using Epias.Data.Interfaces;
using Epias.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Epias.Data.Concrete.EntityFramework.Repostories;

public class EfRepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    protected readonly EpiasDbContext _context;

    public EfRepositoryBase(EpiasDbContext context)
    {
        _context = context;
    }

    public async Task<IList<TEntity>> GetAllByDateAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
        return entity;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }
}

