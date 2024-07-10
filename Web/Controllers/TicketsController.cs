using Asp.Versioning;
using Core.Entites;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
public class TicketsController : ControllerBase
{
  private readonly TicketService _ticketService;

  public TicketsController(TicketService ticketService)
  {
    _ticketService = ticketService;
  }


  [Route("GetTickets")]
  [HttpGet]
  public async Task<List<Ticket>> GetTickets()
  {
    return await _ticketService.GetList();
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
    var response = await _ticketService.AgregarSolucion(solucion);
    if (response.Success == false)
      return UnprocessableEntity();
    return Ok();
  }


  [Route("ActualizarTicket/{id}")]
  [HttpPut]
  public async Task<IActionResult> Put(int id, Ticket ticket)
  {
    var response = await _ticketService.Edit(ticket);
    if (response.Success == false)
      return UnprocessableEntity();
    return Ok();
  }
}
