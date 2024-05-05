using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Compras.Models;

namespace Compras.Repositories;

public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : BaseEntity<TId>
{
    private readonly AppDbContext _context;
    protected DbSet<T> _dbSet;

    protected GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Insert(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T?> GetById(TId id) => await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public bool Any(Expression<Func<T, bool>> anyExpression)
    {
        return _dbSet.Any(anyExpression);
    }
}
