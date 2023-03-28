using System;
using ApplicationCore.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.IServices.CRUD
{
	public class CRUD<TEntity> : ICRUD<TEntity> where TEntity : class, new()
    {
        private readonly IRepository _repo;
        private readonly ILogger _logger;

        public CRUD(IRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<TEntity>> GetList()
        {
            var list = new List<TEntity>();
            try
            {
                list = await _repo.Generic<TEntity>().GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return list;
        }

        public async Task<TEntity> Get(int id)
        {
            var model = new TEntity();
            try
            {
                model = await _repo.Generic<TEntity>().BuscarPorId(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return model;
        }

        public async Task<Response> Create(TEntity entity)
        {
            var response = new Response();
            try
            {
                if (await _repo.Generic<TEntity>().Create(entity) > 0)
                {
                    response.Success = true;
                    response.Message = "Agregado correctamente";
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }

        public async Task<Response> Update(TEntity entity)
        {
            var response = new Response();
            try
            {
                if (await _repo.Generic<TEntity>().Update(entity) > 0)
                {
                    response.Success = true;
                    response.Message = "Actualizado correctamente";
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }

        public async Task<Response> Delete(int id)
        {
            var response = new Response();
            try
            {
                var entity = await _repo.Generic<TEntity>().BuscarPorId(id);
                if (await _repo.Generic<TEntity>().Delete(entity) > 0)
                {
                    response.Success = true;
                    response.Message = "Eliminado correctamente";
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }
    }
}

