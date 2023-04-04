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
        public async Task<ActionResult<Ticket>> CrearTicket(Ticket ticket)
        {
            var response = await _ticketService.Create(ticket, null, "Computadora,Computadora.Empleado");

            if (response.Success == false)
                return UnprocessableEntity();

            return ticket;
        }


        [Route("AgregarSolucion/{id}")]
        [HttpPost]
        public async Task<ActionResult<Solucion>> AgregarSolucion(int id, Solucion solucion)
        {
            if (id != solucion.IdTicket)
                return BadRequest();

            var response = await _ticketService.AgregarSolucion(solucion);

            if (response.Success == false)
                return UnprocessableEntity();

            return solucion;
        }


        [Route("ActualizarTicket/{id}")]
        [HttpPut]
        public async Task<ActionResult<Ticket>> Put(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            var response = await _ticketService.Update(ticket, null, "Computadora,Computadora.Empleado");

            if (response.Success == false)
                return UnprocessableEntity();

            return ticket;
        }
    }
}
