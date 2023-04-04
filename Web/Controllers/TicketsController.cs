using Microsoft.AspNetCore.Mvc;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;
using ApplicationCore.IServices;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [Route("GetTickets")]
        [HttpGet]
        public async Task<List<Ticket>> GetTickets()
        {
            return await _ticketService.GetList("Computadora,Computadora.Empleado");
        }


        [Route("GetTickets/{id}")]
        [HttpGet]
        public async Task<ActionResult<Ticket>> GetTickets(int id)
        {
            var ticket = await _ticketService.GetOrNull(id);
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
            var response = await _ticketService.Create(ticket);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }


        [Route("AgregarSolucion/{id}")]
        [HttpPost]
        public async Task<IActionResult> AgregarSolucion(int id, Solucion solucion)
        {
            if (id != solucion.IdTicket)
                return BadRequest();

            var response = await _ticketService.AgregarSolucion(solucion);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }


        [Route("ActualizarTicket/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            var response = await _ticketService.Update(ticket);

            if (response.Success == false)
                return UnprocessableEntity();

            return Ok();
        }
    }
}
