﻿using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly OpenTicketsContext _context;
        private DbSet<TEntity> entities;

        public GenericRepository(OpenTicketsContext context)
        {
            _context = context;
            entities = context.Set<TEntity>();
        }

        public async Task<int> Create(TEntity entity)
        {
            await entities.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Create(List<TEntity> entity)
        {
            await entities.AddRangeAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int? id)
        {
            var entity = await this.entities.FindAsync(id);
            if(entity != null)
              entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(TEntity entity)
        {
            entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetList()
        {
            return await entities.ToListAsync();
        }
        
        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda)
        {
            return await entities.Where(lambda).ToListAsync();
        }

        public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await entities.Select(select).ToListAsync();
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> lambda, string relationships)
        {
            if (String.IsNullOrEmpty(relationships))
            {
                throw new Exception("Relationships string is empty!");
            }
            return await entities.AsQueryable().GetRelationships(relationships).Where(lambda).ToListAsync();
        }

        public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await entities.Where(lambda).Select(select).ToListAsync();
        }

        public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class
        {
            return await entities.AsQueryable().GetRelationships(relationships).Where(lambda).Select(select).ToListAsync();
        }

        public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, TType>> select, string relationships) where TType : class
        {
            return await entities.AsQueryable().GetRelationships(relationships).Select(select).ToListAsync();
        }

        public async Task<List<TType>> GetListIgnoreFilter<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships) where TType : class
        {
            return await entities.IgnoreQueryFilters().AsQueryable().GetRelationships(relationships).Where(lambda).Select(select).ToListAsync();
        }

        public async Task<List<TType>> GetList<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relationships, string relationships2) where TType : class
        {
            return await entities.AsQueryable().GetRelationships(relationships).GetRelationships(relationships2).Where(lambda).Select(select).ToListAsync();
        }

        public async Task<TEntity?> BuscarPorId(int? id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<TEntity?> BuscarUnElemento(Expression<Func<TEntity, bool>> lambda)
        {
            return await entities.FirstOrDefaultAsync(lambda);
        }

        public async Task<TType?> BuscarUnElemento<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await entities.AsQueryable().Where(lambda).Select(select).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> BuscarUnElemento(Expression<Func<TEntity, bool>> lambda, string relationships)
        {
            if (String.IsNullOrEmpty(relationships))
            {
                throw new Exception("Relationships string is empty!");
            }
            return await entities.AsQueryable().GetRelationships(relationships).FirstOrDefaultAsync(lambda);
        }

        public async Task<TEntity?> BuscarUnElementoIgnoreFilter(Expression<Func<TEntity, bool>> lambda, string relationships)
        {
            if (String.IsNullOrEmpty(relationships))
            {
                throw new Exception("Relationships string is empty!");
            }
            return await entities.IgnoreQueryFilters().AsQueryable().GetRelationships(relationships).FirstOrDefaultAsync(lambda);
        }

        public async Task<TType?> BuscarUnElemento<TType>(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, TType>> select, string relaciones) where TType : class
        {
            if (String.IsNullOrEmpty(relaciones))
            {
                throw new Exception("Relationships string is empty!");
            }

            return await entities.AsQueryable().GetRelationships(relaciones)
                .Where(lambda).Select(select).FirstOrDefaultAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            entities.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(List<TEntity> entity)
        {
            entities.UpdateRange(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ElementExists(Expression<Func<TEntity, bool>> lambda)
        {
            return await entities.AsQueryable().AnyAsync(lambda);
        }

    }

    public static class GenericRepositoryExtensions
    {
        public static IQueryable<TEntity> GetRelationships<TEntity>(this IQueryable<TEntity> entities, string includedProperties) where TEntity : class
        {
            // var entidades = this.entities.AsQueryable();
            var relations = includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in relations)
            {
                entities = entities.Include(property);
            }

            return entities;
        }
    }
}
