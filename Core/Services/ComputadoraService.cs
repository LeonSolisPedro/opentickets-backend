using Core.Dto;
using Core.Entites;
using Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class ComputadoraService
{
  private readonly IGenericRepository<Computadora> _genericRepository;
  private readonly IComputadoraRepository _repoComputadora;
  private readonly ILogger<ComputadoraService> _logger;

  public ComputadoraService(IGenericRepository<Computadora> genericRepository, IComputadoraRepository repoComputadora, ILogger<ComputadoraService> logger)
  {
    _genericRepository = genericRepository;
    _repoComputadora = repoComputadora;
    _logger = logger;
  }

  public async Task<List<Computadora>> GetList()
  {
    var list = new List<Computadora>();
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


  public async Task<Computadora?> GetOrNull(int id)
  {
    Computadora? computadora = null;
    try
    {
      computadora = await _genericRepository.GetOrNull(id);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
    }
    return computadora;
  }


  public async Task<Response> Create(Computadora computadora)
  {
    var response = new Response();
    try
    {
      _genericRepository.Create(computadora);
      await _genericRepository.SaveChanges();
      response.Success = true;
      response.Message = "Computadora agregada correctamente";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
      response.Message = "Error";
    }
    return response;
  }


  public async Task<Response> Edit(Computadora computadora)
  {
    var response = new Response();
    try
    {
      _genericRepository.Edit(computadora);
      await _genericRepository.SaveChanges();
      response.Success = true;
      response.Message = "Computadora editada correctamente";
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
      var model = await _genericRepository.GetOrNull(x => x.Id == id, "Empleado");
      if (model?.Empleado != null || model == null)
      {
        return new Response { Message = "No se puede eliminar, un empleado está asignado a esta computadora" };
      }
      _genericRepository.Delete(model);
      await _genericRepository.SaveChanges();
      response.Success = true;
      response.Message = "Computadora editada correctamente";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
      response.Message = "Error";
    }
    return response;

  }

  public async Task<List<Computadora>> GetComputadorasDropdown(string? empleados)
  {
    var list = new List<Computadora>();
    try
    {
      list = await _genericRepository.GetList("Empleado");

      if (empleados == "asignados")
        list = list.Where(x => x.Empleado != null).ToList();

      if (empleados == "noasignados")
        list = list.Where(x => x.Empleado == null).ToList();

      return list;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
    }
    return list;
  }


  public async Task<string> SayHi()
  {
    try
    {
      var uwu = await _repoComputadora.GetPrimerCompu();
      var hayvoy = await _genericRepository.GetList();
      return $"Hello from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error");
    }
    return "Error";
  }


}
