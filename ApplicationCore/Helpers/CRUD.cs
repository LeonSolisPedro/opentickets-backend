using System;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;

namespace ApplicationCore.Helpers
{
	public class CRUD<TEntity> : ICRUD<TEntity> where TEntity : class
    {
        private readonly IRepository _repo;

        public CRUD(IRepository repo)
        {
            _repo = repo;
        }

        public Task<List<TEntity>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Create(TEntity computadora)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(TEntity computadora)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

