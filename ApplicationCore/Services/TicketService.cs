using System;
using ApplicationCore.Helpers;
using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class TicketService : GenericService<Ticket>, ITicketService
    {
        private readonly IRepository _repo;
        private readonly ILogger<TicketService> _logger;


        public TicketService(IRepository repo, ILogger<TicketService> logger) : base(repo, logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Ticket>> GetTicketsPorIdCompu(int idCompu)
        {
            var list = new List<Ticket>();
            try
            {
                list = await _repo.Generic<Ticket>().GetList(x => x.IdComputadora == idCompu, "Solucion");
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
                await _repo.Generic<Solucion>().Create(solucion);
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

