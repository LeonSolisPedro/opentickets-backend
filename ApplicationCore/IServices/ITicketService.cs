using ApplicationCore.Helpers;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;

namespace ApplicationCore.IServices
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu);
        Task<Response> AgregarSolucion(Solucion solucion);

    }
}

