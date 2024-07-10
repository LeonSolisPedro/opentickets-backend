

using System.Linq.Expressions;

namespace Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Create(TEntity entity);

    void Create(List<TEntity> entity);

    void Edit(TEntity entity);

    void Edit(List<TEntity> entity);
    
    void Delete(TEntity entity);

    Task DeleteById(int id);

    Task<TEntity?> GetOrNull(Expression<Func<TEntity, bool>> lambda);

    Task<TType?> GetOrNull<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class;

    Task<TType?> GetOrNull<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relaciones) where TType : class;

    Task<TEntity?> GetOrNull(Expression<Func<TEntity, bool>> lambda, string relationships);

    Task<TEntity?> GetOrNullIgnoreFilter(Expression<Func<TEntity, bool>> lambda, string relationships);

    Task<TEntity?> GetOrNull(int id);

    Task<List<TEntity>> GetList();

    Task<List<TEntity>> GetList(string relationships);

    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda);

    Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select) where TType : class;

    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda, string relationships);

    Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class;

    Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class;

    Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select, string relationships) where TType : class;

    Task<List<TType>> GetListIgnoreFilter<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class;

    Task<bool> ElementExists(Expression<Func<TEntity, bool>> lambda);

    Task<int> SaveChanges();

}
