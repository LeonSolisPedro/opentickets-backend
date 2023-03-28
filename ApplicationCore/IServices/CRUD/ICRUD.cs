using System;
using System.Linq.Expressions;
using ApplicationCore.Helpers;

namespace ApplicationCore.IServices.CRUD
{
	public interface ICRUD<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetList(string relationships = "");
        Task<TEntity> Get(int id, string relationships = "");
        Task<TEntity?> GetOrNull(int id, string relationships = "");
        Task<Response> Create(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null);
        Task<Response> Update(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null);
        Task<Response> Delete(int id, string relationships = "", string dependant = "");
    }
}

