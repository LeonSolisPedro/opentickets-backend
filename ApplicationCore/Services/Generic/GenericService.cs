﻿using ApplicationCore.Helpers;
using ApplicationCore.IServices.Generic;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ApplicationCore.Services.Generic
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, new()
    {
        private readonly GenericRepository<TEntity> _repo;
        private readonly ILogger<GenericService<TEntity>> _logger;

        public GenericService(OpenTicketsContext context, ILogger<GenericService<TEntity>> logger)
        {
            _repo = new GenericRepository<TEntity>(context);
            _logger = logger;
        }

        public async Task<List<TEntity>> GetList(string relationships = "")
        {
            var list = new List<TEntity>();
            try
            {
                list = await _repo.GetList(relationships);
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
                model = await _repo.GetOrNull(id, relationships) ?? new TEntity();
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
                model = await _repo.GetOrNull(id, relationships);
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
                if (nameExists != null)
                {
                    var exists = await _repo.ElementExists(nameExists);
                    if (exists)
                    {
                        response.Message = "Ya existe un elemento con este nombre, intente con uno nuevo";
                        return response;
                    }
                }
                await _repo.Create(entity);
                response.Success = true;
                response.Message = "Elemento agregado correctamente";
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
                    var exists = await _repo.ElementExists(nameExists);
                    if (exists)
                    {
                        response.Message = "Ya existe un elemento con este nombre, intente con uno nuevo";
                        return response;
                    }
                }
                await _repo.Update(entity);
                response.Success = true;
                response.Message = "Elemento actualizado correctamente";
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
                var model = await _repo.GetOrNull(id, relationships) ?? new TEntity();
                if (dependant != "")
                {
                    if (PropertyExists(model, dependant) != null)
                    {
                        response.Message = "El elemento está asignado a otros elementos, lo que evita su eliminación";
                        return response;
                    }
                }
                await _repo.Delete(model);
                response.Success = true;
                response.Message = "Elemento eliminado correctamente";
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

