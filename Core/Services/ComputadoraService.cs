using Core.Dto;
using Core.Entites;
using Core.Repositories;

namespace Core.Services;

public class ComputadoraService
{
  private readonly IGenericRepository<Computadora> _genericRepository;
  private readonly IComputadoraRepository _repoComputadora;

  public ComputadoraService(IGenericRepository<Computadora> genericRepository, IComputadoraRepository repoComputadora)
  {
    _genericRepository = genericRepository;
    _repoComputadora = repoComputadora;
  }

  public async Task<List<Computadora>> GetList()
  {
    return await _genericRepository.GetList();
  }


  public async Task<Computadora?> GetOrNull(int id)
  {
    return await _genericRepository.GetOrNull(id);
  }


  public async Task<Response> Create(Computadora computadora)
  {
    _genericRepository.Create(computadora);
    await _genericRepository.SaveChanges();
    return new Response
    {
      Success = true,
      Message = "Computadora agregada correctamente"
    };
  }


  public async Task<Response> Edit(Computadora computadora)
  {
    _genericRepository.Edit(computadora);
    await _genericRepository.SaveChanges();
    return new Response
    {
      Success = true,
      Message = "Computadora editada correctamente"
    };
  }


  public async Task<Response> Delete(int id)
  {
    var model = await _genericRepository.GetOrNull(x => x.Id == id, "Empleado");
    if (model?.Empleado != null || model == null)
    {
      return new Response { Message = "No se puede eliminar, un empleado está asignado a esta computadora" };
    }
    _genericRepository.Delete(model);
    await _genericRepository.SaveChanges();
    return new Response
    {
      Success = true,
      Message = "Computadora editada correctamente"
    };
  }

  public async Task<List<Computadora>> GetComputadorasDropdown(string? empleados)
  {
    var list = await _genericRepository.GetList("Empleado");

    if (empleados == "asignados")
      list = list.Where(x => x.Empleado != null).ToList();

    if (empleados == "noasignados")
      list = list.Where(x => x.Empleado == null).ToList();

    return list;
  }


  public async Task<string> SayHi()
  {
    var uwu = await _repoComputadora.GetPrimerCompu();
    var hayvoy = await _genericRepository.GetList();
    return $"Hello from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
  }


}
