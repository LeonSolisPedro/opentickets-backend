using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IGenericService<Empleado> _genericService;

        public EmpleadosController(IGenericService<Empleado> genericService)
        {
            _genericService = genericService;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await _genericService.GetList();
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _genericService.GetOrNull(id, "Computadora");
            if (empleado == null)
                return NotFound();
            return empleado;
        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Empleado>> PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.Id)
                return BadRequest();

            var response = await _genericService.Update(empleado);

            if (response.Success == false)
                return UnprocessableEntity();

            return empleado;
        }

        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {

            var response = await _genericService.Create(empleado);

            if (response.Success == false)
                return UnprocessableEntity();

            return empleado;
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var response = await _genericService.Delete(id);

            if (response.Success == false)
                return UnprocessableEntity();

            return NoContent();
        }
    }
}
