using Core.Entites;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ComputadoraRepository : IComputadoraRepository
{

    private readonly AppDbContext _context;

    public ComputadoraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Computadora> GetPrimerCompu()
    {
        return await _context.Computadoras.FirstOrDefaultAsync() ?? new Computadora();
    }
}

