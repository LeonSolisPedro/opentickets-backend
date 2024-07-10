using Asp.Versioning;
using Core.Entites;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
public class EmpleadosController : ControllerBase
{
  private readonly EmpleadoService _empleadoService;

  public EmpleadosController(EmpleadoService empleadoService)
  {
    _empleadoService = empleadoService;
  }


  [HttpGet]
  public async Task<List<Empleado>> Get()
  {
    return await _empleadoService.GetList();
  }


  [HttpGet("{id}")]
  public async Task<ActionResult<Empleado>> Get(int id)
  {
    var empleado = await _empleadoService.GetOrNull(id);
    if (empleado == null)
      return NotFound();
    return empleado;
  }


  [HttpPost]
  public async Task<IActionResult> Post(Empleado empleado)
  {
    var response = await _empleadoService.Create(empleado);
    if (response.Success == false)
      return UnprocessableEntity();
    return Ok();
  }


  [HttpPut("{id}")]
  public async Task<IActionResult> Put(int id, Empleado empleado)
  {
    var response = await _empleadoService.Edit(empleado);
    if (response.Success == false)
      return UnprocessableEntity();
    return Ok();
  }


  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var response = await _empleadoService.Delete(id);
    if (response.Success == false)
      return UnprocessableEntity();
    return Ok();
  }
}
