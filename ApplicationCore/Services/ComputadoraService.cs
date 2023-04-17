using ApplicationCore.IServices;
using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class ComputadoraService : IComputadoraService
    {

        private readonly GenericRepository<Computadora> _repo;
        private readonly ComputadoraRepository _repoComputadora;
        private readonly ILogger<ComputadoraService> _logger;

        public ComputadoraService(OpenTicketsContext context, ILogger<ComputadoraService> logger)
        {
            _repo = new GenericRepository<Computadora>(context);
            _repoComputadora = new ComputadoraRepository(context);
            _logger = logger;
        }

        public async Task<List<Computadora>> GetComputadorasDropdown(string? empleados)
        {
            var list = new List<Computadora>();
            try
            {
                list = await _repo.GetList("Empleado");

                if (empleados == "asignados")
                    list = list.Where(x => x.Empleado != null).ToList();

                if (empleados == "noasignados")
                    list = list.Where(x => x.Empleado == null).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return list;
        }

        public async Task<string> SayHi()
        {
            var uwu = await _repoComputadora.GetPrimerCompu();
            var hayvoy = await _repo.GetList();
            return $"Hello from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
        }
    }
}
