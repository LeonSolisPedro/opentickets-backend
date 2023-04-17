using ApplicationCore.Helpers;
using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class TicketService : GenericService<Ticket>, ITicketService
    {
        private readonly GenericRepository<Ticket> _repo;
        private readonly GenericRepository<Solucion> _repoSolucion;
        private readonly ILogger<TicketService> _logger;


        public TicketService(OpenTicketsContext context, ILogger<TicketService> logger) : base(context, logger)
        {
            _repo = new GenericRepository<Ticket>(context);
            _repoSolucion = new GenericRepository<Solucion>(context);
            _logger = logger;
        }

        public async Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu)
        {
            var list = new List<Ticket>();
            try
            {
                list = await _repo.GetList(x => x.IdComputadora == idCompu, "Solucion");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return list;
        }

        public async Task<Response> AgregarSolucion(Solucion solucion)
        {
            var response = new Response();
            try
            {
                await _repoSolucion.Create(solucion);
                response.Success = true;
                response.Message = "Solución aggregada correctamente";
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return response;
        }
    }
}

