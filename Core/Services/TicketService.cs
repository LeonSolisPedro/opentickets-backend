using Core.Dto;
using Core.Entites;
using Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class TicketService
{
    private readonly IGenericRepository<Ticket> _genericRepository;

    private readonly IGenericRepository<Solucion> _repoSolucion;

    private readonly ILogger<TicketService> _logger;

    public TicketService(IGenericRepository<Ticket> genericRepository, IGenericRepository<Solucion> repoSolucion, ILogger<TicketService> logger)
    {
        _genericRepository = genericRepository;
        _repoSolucion = repoSolucion;
        _logger = logger;
    }


    public async Task<List<Ticket>> GetList()
    {
        var list = new List<Ticket>();
        try
        {
            list = await _genericRepository.GetList("Computadora,Computadora.Empleado,Solucion");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
        }
        return list;
    }


    public async Task<Ticket?> GetOrNull(int id)
    {
        Ticket? ticket = null;
        try
        {
            ticket = await _genericRepository.GetOrNull(x => x.Id == id, "Solucion");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
        }
        return ticket;
    }


    public async Task<Response> Create(Ticket ticket)
    {
        var response = new Response();
        try
        {
            _genericRepository.Create(ticket);
            await _genericRepository.SaveChanges();
            response.Success = true;
            response.Message = "Ticket creado con éxito";
        }
        catch (Exception e)
        {
          _logger.LogError(e, "Error");
          response.Message = "Error";
        }
        return response;
    }


    public async Task<Response> Edit(Ticket ticket)
    {
        var response = new Response();
        try
        {
            _genericRepository.Edit(ticket);
            await _genericRepository.SaveChanges();
            response.Success = true;
            response.Message = "Ticket editado con éxito";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            response.Message = "Error";
        }
        return response;
    }


    public async Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu)
    {
        var list = new List<Ticket>();
        try
        {
            list = await _genericRepository.GetList(x => x.IdComputadora == idCompu, "Solucion");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
        }
        return list;
    }


    public async Task<Response> AgregarSolucion(Solucion solucion)
    {
        var response = new Response();
        try
        {
            _repoSolucion.Create(solucion);
            await _genericRepository.SaveChanges();
            response.Success = true;
            response.Message = "Solución agregada correctamente";
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error");
            response.Message = "Error";
        }
        return response;
    }
}
