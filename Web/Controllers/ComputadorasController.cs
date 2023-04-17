using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputadorasController : ControllerBase
    {
        private readonly IGenericService<Computadora> _genericService;
        private readonly IComputadoraService _computadoraService;

        public ComputadorasController(IGenericService<Computadora> genericService, IComputadoraService computadoraService)
        {
            _genericService = genericService;
            _computadoraService = computadoraService;
        }


        [Route("GetComputadoras")]
        [HttpGet]
        public async Task<List<Computadora>> GetComputadoras()
        {
            return await _genericService.GetList();
        }


        [Route("GetComputadoras/{id}")]
        [HttpGet]
        public async Task<ActionResult<Computadora>> GetComputadoras(int id)
        {
            var computadora = await _genericService.GetOrNull(id);
            if (computadora == null)
                return NotFound();
            return computadora;
        }


        [Route("GetComputadorasDropdown")]
        [HttpGet]
        public async Task<dynamic> GetComputadorasDropdown(string? empleados)
        {
            var list = await _computadoraService.GetComputadorasDropdown(empleados);
            return list.Select(x => new { NombreEmpleado = x.Empleado?.NombreEmpleado, NombreComputadora = x.MarcaModel, IdComputadora = x.Id });
        }


        [Route("SayHi")]
        [HttpGet]
        public async Task<string> SayHi()
        {
            return await _computadoraService.SayHi();
        }


        [HttpPost]
        public async Task<IActionResult> Post(Computadora computadora)
        {
            var response = await _genericService.Create(computadora);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Computadora computadora)
        {
            var response = await _genericService.Update(computadora);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _genericService.Delete(id, "Empleado", "Empleado");
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }
    }
}