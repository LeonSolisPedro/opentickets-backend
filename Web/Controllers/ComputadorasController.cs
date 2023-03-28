using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.IServices;
using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: /Computadoras
        [Route("GetComputadoras")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Computadora>>> GetComputadoras()
        {
            return await _computadoraService.GetList();
        }

        // GET: /Computadoras/5
        [Route("GetComputadora/{id}")]
        [HttpGet]
        public async Task<ActionResult<Computadora>> GetComputadora(int id)
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

        // PUT: /Computadoras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Computadora>> PutComputadora(int id, Computadora computadora)
        {
            if (id != computadora.Id)
                return BadRequest();

            var response = await _computadoraService.Update(computadora);

            if (response.Success == false)
                return UnprocessableEntity();

            return computadora;
        }

        // POST: /Computadoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Computadora>> PostComputadora(Computadora computadora)
        {
            var response = await _computadoraService.Create(computadora);

            if (response.Success == false)
                return UnprocessableEntity();

            return computadora;
        }

        // DELETE: /Computadoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputadora(int id)
        {
            var response = await _computadoraService.Delete(id, "Empleado", "Empleado");

            if (response.Success == false)
                return UnprocessableEntity();

            return NoContent();
        }
    }
}
