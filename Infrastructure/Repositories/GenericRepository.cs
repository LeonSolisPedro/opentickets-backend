using System.Linq.Expressions;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{

    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Create(List<TEntity> entity)
    {
        _context.Set<TEntity>().AddRange(entity);
    }

    public void Edit(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<TEntity>().Update(entity);
    }

    public void Edit(List<TEntity> entity)
    {
        _context.Set<TEntity>().UpdateRange(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task DeleteById(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
            _context.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> GetOrNull(Expression<Func<TEntity, bool>> lambda)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(lambda);
    }

    public async Task<TType?> GetOrNull<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class
    {
        return await _context.Set<TEntity>().AsQueryable().Where(lambda).Select(select).FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetOrNull(Expression<Func<TEntity, bool>> lambda, string relationships)
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).FirstOrDefaultAsync(lambda);
    }

    public async Task<TEntity?> GetOrNullIgnoreFilter(Expression<Func<TEntity, bool>> lambda, string relationships)
    {
        return await _context.Set<TEntity>().IgnoreQueryFilters().AsQueryable().GetRelationships(relationships).FirstOrDefaultAsync(lambda);
    }

    public async Task<TType?> GetOrNull<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relaciones) where TType : class
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relaciones).Where(lambda).Select(select).FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetOrNull(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> GetList()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<List<TEntity>> GetList(string relationships)
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).ToListAsync();
    }

    public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda)
    {
        return await _context.Set<TEntity>().Where(lambda).ToListAsync();
    }

    public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select) where TType : class
    {
        return await _context.Set<TEntity>().Select(select).ToListAsync();
    }

    public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda, string relationships)
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).Where(lambda).ToListAsync();
    }

    public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class
    {
        return await _context.Set<TEntity>().Where(lambda).Select(select).ToListAsync();
    }

    public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).Where(lambda).Select(select).ToListAsync();
    }

    public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select, string relationships) where TType : class
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).Select(select).ToListAsync();
    }

    public async Task<List<TType>> GetListIgnoreFilter<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class
    {
        return await _context.Set<TEntity>().IgnoreQueryFilters().AsQueryable().GetRelationships(relationships).Where(lambda).Select(select).ToListAsync();
    }

    public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships, string relationships2) where TType : class
    {
        return await _context.Set<TEntity>().AsQueryable().GetRelationships(relationships).GetRelationships(relationships2).Where(lambda).Select(select).ToListAsync();
    }

    public async Task<bool> ElementExists(Expression<Func<TEntity, bool>> lambda)
    {
        return await _context.Set<TEntity>().AsQueryable().AnyAsync(lambda);
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

}


public static class GenericRepositoryExtensions
{
    public static IQueryable<TEntity> GetRelationships<TEntity>(this IQueryable<TEntity> entities, string includedProperties) where TEntity : class
    {
        var relations = includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var property in relations)
        {
            entities = entities.Include(property);
        }

        return entities;
    }
}
