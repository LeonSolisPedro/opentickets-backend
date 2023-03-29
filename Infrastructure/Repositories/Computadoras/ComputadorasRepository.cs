using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Computadoras
{
    public class ComputadorasRepository
    {

        private readonly OpenTicketsContext _context;
        
        public ComputadorasRepository(OpenTicketsContext context)
        {
            _context = context;
        }

        public async Task<Computadora> GetPrimerCompu()
        {
            return await _context.Computadoras.FirstOrDefaultAsync() ?? new Computadora();
        }
    }
}
