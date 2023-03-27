using Infrastructure.Context;
using Infrastructure.Repositories.Computadoras;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    // This is an Unit Of Work Implementation.
    // Ef core already implements Unit Of Work
    // but we're making this, so our code is
    // better to read and maintain.
    // See: https://t.ly/mf9W, https://t.ly/-bTA
    // And: https://t.ly/NC8S, https://t.ly/HXnk
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
    }
}
