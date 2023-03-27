using System;
namespace ApplicationCore.Helpers
{
	public interface ICRUD<TEntity> where TEntity: class
	{
        Task<List<TEntity>> GetList();
        Task<TEntity> Get(int id);
        Task<Response> Create(TEntity entity);
        Task<Response> Update(TEntity entity);
        Task<Response> Delete(int id);
    }
}

