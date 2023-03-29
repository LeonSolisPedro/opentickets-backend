using System;
using ApplicationCore.Helpers;
using ApplicationCore.IServices.CRUD;
using Infrastructure.Models;

namespace ApplicationCore.IServices
{
	public interface ITicketService : ICRUD<Ticket>
	{
		Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu);
		Task<Response> AgregarSolucion(Solucion solucion);

    }
}

