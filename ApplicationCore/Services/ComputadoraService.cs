using ApplicationCore.Helpers;
using ApplicationCore.IServices;
using ApplicationCore.IServices.CRUD;
using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ComputadoraService : CRUD<Computadora>, IComputadoraService
    {

        private readonly IRepository _repo;
        private readonly ILogger<ComputadoraService> _logger;

        public ComputadoraService(IRepository repo, ILogger<ComputadoraService> logger) : base(repo, logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Computadora>> GetComputadorasDropdown(string? empleados)
        {
            var list = new List<Computadora>();
            try
            {
                list = await _repo.Generic<Computadora>().GetList("Empleado");

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
            var uwu = await _repo.Computadoras.GetPrimerCompu();
            var hayvoy = await _repo.Generic<Computadora>().GetList();
            return $"Hello from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
        }
    }
}
