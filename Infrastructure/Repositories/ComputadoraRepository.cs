using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ComputadoraRepository
    {

        private readonly OpenTicketsContext _context;

        public ComputadoraRepository(OpenTicketsContext context)
        {
            _context = context;
        }

        public async Task<Computadora> GetPrimerCompu()
        {
            return await _context.Computadoras.FirstOrDefaultAsync() ?? new Computadora();
        }
    }
}
