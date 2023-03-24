using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    //This is an Unit Of Work Implementation
    //See: https://t.ly/mf9W, https://t.ly/-bTA
    //And: https://t.ly/NC8S
    public class Repository : IRepository
    {

        private readonly OpenTicketsContext _context;

        public Repository(OpenTicketsContext context)
        {
            _context = context;
            Computadoras = new ComputadorasRepository(context);
        }

        public GenericRepository<TEntity> Generic<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public ComputadorasRepository Computadoras { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
