using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opentickets_backend.Data;

namespace opentickets_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly OpenTicketsContext _context;

        public TicketsController(OpenTicketsContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [Route("GetTickets")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/5
        [Route("GetTicket/{id}")]
        [HttpGet]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [Route("GetTicketsPorIdCompu/{id}")]
        [HttpGet]
        public async Task<List<Ticket>> GetTicketsPorIdCompu(int id)
        {
            var tickets = await _context.Tickets.Include(x => x.Solucion).Where(x => x.IdComputadora == id).ToListAsync();
            return tickets;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("ActualizarTicket/{id}")]
        [HttpPut]
        public async Task<ActionResult<Ticket>> ActualizarTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return ticket;
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("CrearTicket")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> CrearTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'OpenTicketsContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }


        // POST: api/Tickets/AgregarSolucion/5
        [Route("AgregarSolucion/{id}")]
        [HttpPost]
        public async Task<ActionResult<Solucion>> AgregarSolucion(int id, Solucion solucion)
        {

            if (id != solucion.IdTicket)
                return BadRequest();

            if (_context.Soluciones.Any(x => x.IdTicket == id))
                return UnprocessableEntity();

            _context.Soluciones.Add(solucion);
            await _context.SaveChangesAsync();

            return solucion;
        }

        // DELETE: api/Tickets/5
        [Route("EliminarTicket/{id}")]
        [HttpDelete]
        public async Task<IActionResult> EliminarTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
