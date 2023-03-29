using System;
using System.Linq.Expressions;
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

        public async Task<List<TEntity>> GetList(string relationships = "")
        {
            var list = new List<TEntity>();
            try
            {
                list = await _repo.Generic<TEntity>().GetList(relationships);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return list;
        }

        public async Task<TEntity> Get(int id, string relationships = "")
        {
            var model = new TEntity();
            try
            {
                model = await _repo.Generic<TEntity>().GetOrNull(id, relationships) ?? new TEntity();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return model;
        }

        public async Task<TEntity?> GetOrNull(int id, string relationships = "")
        {
            var model = default(TEntity);
            try
            {
                model = await _repo.Generic<TEntity>().GetOrNull(id, relationships);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return model;
        }

        public async Task<Response> Create(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null)
        {
            var response = new Response();
            try
            {
                if(nameExists != null)
                {
                    var exists = await _repo.Generic<TEntity>().ElementExists(nameExists);
                    if (exists)
                    {
                        response.Message = "Ya existe un elemento con este nombre, intente con uno nuevo";
                        return response;
                    }
                }
                await _repo.Generic<TEntity>().Create(entity);
                response.Success = true;
                response.Message = "Agregado correctamente";
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }

        public async Task<Response> Update(TEntity entity, Expression<Func<TEntity, bool>>? nameExists = null)
        {
            var response = new Response();
            try
            {
                if (nameExists != null)
                {
                    var exists = await _repo.Generic<TEntity>().ElementExists(nameExists);
                    if (exists)
                    {
                        response.Message = "Ya existe un elemento con este nombre, intente con uno nuevo";
                        return response;
                    }
                }
                await _repo.Generic<TEntity>().Update(entity);
                response.Success = true;
                response.Message = "Actualizado correctamente";
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }

        public async Task<Response> Delete(int id, string relationships = "", string dependant = "")
        {
            var response = new Response();
            try
            {
                var model = await _repo.Generic<TEntity>().GetOrNull(id, relationships) ?? new TEntity();
                if(dependant != "")
                {
                    if(PropertyExists(model, dependant) != null)
                    {
                        response.Message = "El elemento está asignado a otros elementos, lo que evita su eliminación";
                        return response;
                    }
                }
                await _repo.Generic<TEntity>().Delete(model);
                response.Success = true;
                response.Message = "Eliminado correctamente";
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }

        private object? PropertyExists(object src, string propName)
        {
            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return PropertyExists(PropertyExists(src, temp[0])!, temp[1]);
            }
            else
            {
                var prop = src?.GetType()?.GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
    }
}

