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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/Tickets
        [Route("GetTickets")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _ticketService.GetList();
        }

        // GET: api/Tickets/5
        [Route("GetTicket/{id}")]
        [HttpGet]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
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

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("ActualizarTicket/{id}")]
        [HttpPut]
        public async Task<ActionResult<Ticket>> ActualizarTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            var response = await _ticketService.Update(ticket);

            if (response.Success == false)
                return UnprocessableEntity();

            return ticket;
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("CrearTicket")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> CrearTicket(Ticket ticket)
        {
            var response = await _ticketService.Create(ticket);

            if (response.Success == false)
                return UnprocessableEntity();

            return ticket;
        }


        // POST: api/Tickets/AgregarSolucion/5
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

        // DELETE: api/Tickets/5
        [Route("EliminarTicket/{id}")]
        [HttpDelete]
        public async Task<IActionResult> EliminarTicket(int id)
        {
            var response = await _ticketService.Delete(id);

            if (response.Success == false)
                return UnprocessableEntity();

            return NoContent();
        }
    }
}
