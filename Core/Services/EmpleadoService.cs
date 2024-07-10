using Core.Dto;
using Core.Entites;
using Core.Repositories;

namespace Core.Services;

public class EmpleadoService
{


  private readonly IGenericRepository<Empleado> _genericRepository;

  public EmpleadoService(IGenericRepository<Empleado> genericRepository)
  {
    _genericRepository = genericRepository;
  }


  public async Task<List<Empleado>> GetList()
  {
    return await _genericRepository.GetList();
  }


  public async Task<Empleado?> GetOrNull(int id)
  {
    return await _genericRepository.GetOrNull(x => x.Id == id, "Computadora");
  }


  public async Task<Response> Create(Empleado empleado)
  {
    _genericRepository.Create(empleado);
    await _genericRepository.SaveChanges();
    return new Response
    {
      Success = true,
      Message = "Empleado agregado correctamente"
    };
  }


  public async Task<Response> Edit(Empleado empleado)
  {
    _genericRepository.Edit(empleado);
    await _genericRepository.SaveChanges();
    return new Response
    {
      Success = true,
      Message = "Empleado editado correctamente"
    };
  }


  public async Task<Response> Delete(int id)
  {
    await _genericRepository.DeleteById(id);
    return new Response
    {
      Success = true,
      Message = "Empleado eliminado correctamente"
    };
  }
}
