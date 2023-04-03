using System;
using System.Linq.Expressions;
using ApplicationCore.Helpers;

namespace ApplicationCore.IServices.Generic
{
	public interface IGenericService<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetList(string relationships = "");
        Task<TEntity> Get(int id, string relationships = "");
        Task<TEntity?> GetOrNull(int id, string relationships = "");
        Task<Response> Create(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null, string relationships = "");
        Task<Response> Update(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null, string relationships = "");
        Task<Response> Delete(int id, string relationships = "", string dependant = "");
    }
}

