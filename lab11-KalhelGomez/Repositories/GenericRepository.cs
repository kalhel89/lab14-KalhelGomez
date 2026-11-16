using Microsoft.EntityFrameworkCore;
using TuApp.Domain.Repositories;
using TuApp.Infrastructure.Context;

namespace lab11_KalhelGomez.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TicketeraContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TicketeraContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(string id)
    {
        // Las PKs del scaffold son Guid; el API trabaja con string
        if (!Guid.TryParse(id, out var guid))
            return null;

        return await _dbSet.FindAsync(guid);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}
