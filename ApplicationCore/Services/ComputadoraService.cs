using ApplicationCore.Helpers;
using ApplicationCore.IServices;
using ApplicationCore.IServices.CRUD;
using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        public Task<List<Computadora>> GetComputadorasDropdown(string? empleados)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SayHi()
        {
            var uwu = await _repo.Computadoras.GetPrimerCompu();
            var hayvoy = await _repo.Generic<Computadora>().GetList();
            return $"Holi from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
        }
    }
}
