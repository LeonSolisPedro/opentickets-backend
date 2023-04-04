using Microsoft.AspNetCore.Mvc;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;

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

        
        [HttpGet]
        public async Task<List<Empleado>> Get()
        {
            return await _genericService.GetList();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await _genericService.GetOrNull(id, "Computadora");
            if (empleado == null)
                return NotFound();
            return empleado;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Empleado empleado)
        {
            var response = await _genericService.Create(empleado);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Empleado empleado)
        {
            if (id != empleado.Id)
                return BadRequest();

            var response = await _genericService.Update(empleado);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _genericService.Delete(id);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }
    }
}
