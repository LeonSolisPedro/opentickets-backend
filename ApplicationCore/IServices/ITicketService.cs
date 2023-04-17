using ApplicationCore.Helpers;
using Infrastructure.Models;

namespace ApplicationCore.IServices
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu);
        Task<Response> AgregarSolucion(Solucion solucion);

    }
}

