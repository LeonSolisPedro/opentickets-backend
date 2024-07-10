using Core.Dto;
using Core.Entites;
using Core.Repositories;

namespace Core.Services;

public class TicketService
{
    private readonly IGenericRepository<Ticket> _genericRepository;

    private readonly IGenericRepository<Solucion> _repoSolucion;

    public TicketService(IGenericRepository<Ticket> genericRepository, IGenericRepository<Solucion> repoSolucion)
    {
        _genericRepository = genericRepository;
        _repoSolucion = repoSolucion;
    }


    public async Task<List<Ticket>> GetList()
    {
        return await _genericRepository.GetList("Computadora,Computadora.Empleado,Solucion");
    }


    public async Task<Ticket?> GetOrNull(int id)
    {
        return await _genericRepository.GetOrNull(x => x.Id == id, "Solucion");
    }


    public async Task<Response> Create(Ticket ticket)
    {
        _genericRepository.Create(ticket);
        await _genericRepository.SaveChanges();
        return new Response
        {
            Success = true,
            Message = "Ticket creado con éxito"
        };
    }


    public async Task<Response> Edit(Ticket ticket)
    {
        _genericRepository.Edit(ticket);
        await _genericRepository.SaveChanges();
        return new Response
        {
            Success = true,
            Message = "Ticket editado con éxito"
        };
    }


    public async Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu)
    {
        return await _genericRepository.GetList(x => x.IdComputadora == idCompu, "Solucion");
    }


    public async Task<Response> AgregarSolucion(Solucion solucion)
    {
        _repoSolucion.Create(solucion);
        await _genericRepository.SaveChanges();
        return new Response
        {
            Success = true,
            Message = "Solución agregada correctamente"
        };
    }
}
