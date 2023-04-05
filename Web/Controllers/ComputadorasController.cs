using Microsoft.AspNetCore.Mvc;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;
using ApplicationCore.IServices;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputadorasController : ControllerBase
    {
        private readonly IComputadoraService _computadoraService;

        public ComputadorasController(IComputadoraService computadoraService)
        {
            _computadoraService = computadoraService;
        }


        [Route("GetComputadoras")]
        [HttpGet]
        public async Task<List<Computadora>> GetComputadoras()
        {
            return await _computadoraService.GetList();
        }


        [Route("GetComputadoras/{id}")]
        [HttpGet]
        public async Task<ActionResult<Computadora>> GetComputadoras(int id)
        {
            var computadora = await _computadoraService.GetOrNull(id);
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
            var response = await _computadoraService.Create(computadora);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Computadora computadora)
        {
            var response = await _computadoraService.Update(computadora);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _computadoraService.Delete(id, "Empleado", "Empleado");
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }
    }
}