using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IGenericService<Ticket> _genericService;
        private readonly ITicketService _ticketService;

        public TicketsController(IGenericService<Ticket> genericService, ITicketService ticketService)
        {
            _genericService = genericService;
            _ticketService = ticketService;
        }


        [Route("GetTickets")]
        [HttpGet]
        public async Task<List<Ticket>> GetTickets()
        {
            return await _genericService.GetList("Computadora,Computadora.Empleado,Solucion");
        }


        [Route("GetTickets/{id}")]
        [HttpGet]
        public async Task<ActionResult<Ticket>> GetTickets(int id)
        {
            var ticket = await _genericService.GetOrNull(id, "Solucion");
            if (ticket == null)
                return NotFound();
            return ticket;
        }


        [Route("GetTicketsPorIdCompu/{id}")]
        [HttpGet]
        public async Task<List<Ticket>> GetTicketsPorIdCompu(int id)
        {
            return await _ticketService.GetTicketsPorIdCompu(id);
        }


        [Route("CrearTicket")]
        [HttpPost]
        public async Task<IActionResult> CrearTicket(Ticket ticket)
        {
            var response = await _genericService.Create(ticket);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }


        [Route("AgregarSolucion/{id}")]
        [HttpPost]
        public async Task<IActionResult> AgregarSolucion(int id, Solucion solucion)
        {
            var response = await _ticketService.AgregarSolucion(solucion);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }


        [Route("ActualizarTicket/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, Ticket ticket)
        {
            var response = await _genericService.Update(ticket);
            if (response.Success == false)
                return UnprocessableEntity();
            return Ok();
        }
    }
}