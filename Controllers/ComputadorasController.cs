using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opentickets_backend.Data;

namespace opentickets_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputadorasController : ControllerBase
    {
        private readonly OpenTicketsContext _context;

        public ComputadorasController(OpenTicketsContext context)
        {
            _context = context;
        }

        // GET: /Computadoras
        [Route("GetComputadoras")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Computadora>>> GetComputadoras()
        {
          if (_context.Computadoras == null)
          {
              return NotFound();
          }
            return await _context.Computadoras.ToListAsync();
        }

        // GET: /Computadoras/5
        [Route("GetComputadora/{id}")]
        [HttpGet]
        public async Task<ActionResult<Computadora>> GetComputadora(int id)
        {
          if (_context.Computadoras == null)
          {
              return NotFound();
          }
            var computadora = await _context.Computadoras.FindAsync(id);

            if (computadora == null)
            {
                return NotFound();
            }

            return computadora;
        }

        [Route("GetTicketsPorCompu/{id}")]
        [HttpGet]
        public async Task<List<Ticket>> GetTicketsPorCompu(int id)
        {
            var tickets = await _context.Computadoras.Include(x => x.Tickets)!.ThenInclude(x => x.Solucion).FirstOrDefaultAsync(x => x.Id == id);
            return tickets?.Tickets ?? new List<Ticket>();
        }

        // PUT: /Computadoras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Computadora>> PutComputadora(int id, Computadora computadora)
        {
            if (id != computadora.Id)
            {
                return BadRequest();
            }

            _context.Entry(computadora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputadoraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return computadora;
        }

        // POST: /Computadoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Computadora>> PostComputadora(Computadora computadora)
        {
          if (_context.Computadoras == null)
          {
              return Problem("Entity set 'OpenTicketsContext.Computadoras'  is null.");
          }
            _context.Computadoras.Add(computadora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComputadora", new { id = computadora.Id }, computadora);
        }

        // DELETE: /Computadoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputadora(int id)
        {
            if (_context.Computadoras == null)
            {
                return NotFound();
            }
            var computadora = await _context.Computadoras.FindAsync(id);
            if (computadora == null)
            {
                return NotFound();
            }

            _context.Computadoras.Remove(computadora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputadoraExists(int id)
        {
            return (_context.Computadoras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
