using System;
using ApplicationCore.Helpers;

namespace ApplicationCore.IServices.CRUD
{
	public interface ICRUD<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetList();
        Task<TEntity> Get(int id);
        Task<Response> Create(TEntity entity);
        Task<Response> Update(TEntity entity);
        Task<Response> Delete(int id);
    }
}

