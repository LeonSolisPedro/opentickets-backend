using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<List<TEntity>> ObtieneLista()
        {
            return await entities.ToListAsync();
        }


    }
}
