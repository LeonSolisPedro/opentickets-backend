﻿using System;
using ApplicationCore.Helpers;

namespace ApplicationCore.IServices.CRUD
{
	public interface ICRUD<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetList(string relationships = "");
        Task<TEntity> Get(int id, string relationships = "");
        Task<TEntity?> GetOrNull(int id, string relationships = "");
        Task<Response> Create(TEntity entity);
        Task<Response> Update(TEntity entity);
        Task<Response> Delete(int id, string relationships = "", string dependant = "");
    }
}

