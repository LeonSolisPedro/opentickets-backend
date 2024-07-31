using Core.Dto;
using Core.Entites;
using Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class EmpleadoService
{


  private readonly IGenericRepository<Empleado> _genericRepository;

  private readonly ILogger<EmpleadoService> _logger;

  public EmpleadoService(IGenericRepository<Empleado> genericRepository, ILogger<EmpleadoService> logger)
  {
    _genericRepository = genericRepository;
    _logger = logger;
  }


  public async Task<List<Empleado>> GetList()
  {
    var list = new List<Empleado>();
    try
    {
      list = await _genericRepository.GetList();
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
    }
    return list;
  }


  public async Task<Empleado?> GetOrNull(int id)
  {
    Empleado? empleado = null;
    try
    {
      empleado = await _genericRepository.GetOrNull(x => x.Id == id, "Computadora");
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
    }
    return empleado;
  }


  public async Task<Response> Create(Empleado empleado)
  {
    var response = new Response();
    try
    {
      _genericRepository.Create(empleado);
      await _genericRepository.SaveChanges();
      response.Success = true;
      response.Message = "Empleado agregado correctamente";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
      response.Message = "Error";
    }
    return response;
  }


  public async Task<Response> Edit(Empleado empleado)
  {
    var response = new Response();
    try
    {
      _genericRepository.Edit(empleado);
      await _genericRepository.SaveChanges();
      response.Success = true;
      response.Message = "Empleado editado correctamente";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
      response.Message = "Error";
    }
    return response;
  }


  public async Task<Response> Delete(int id)
  {
    var response = new Response();
    try
    {
      await _genericRepository.DeleteById(id);
      response.Success = true;
      response.Message = "Empleado eliminado correctamente";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
      response.Message = "Error";
    }
    return response;
  }
}