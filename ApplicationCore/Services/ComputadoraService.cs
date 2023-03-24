using ApplicationCore.IServices;
using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ComputadoraService : IComputadoraService
    {

        private readonly IRepository _repo;

        public ComputadoraService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> SayHi()
        {
            var uwu = await _repo.Computadoras.GetPrimerCompu();
            var hayvoy = await _repo.Generic<Computadora>().ObtieneLista();
            return $"Holi from Computadora Service: {uwu.MarcaModel} - Count: {hayvoy.Count}";
        }
    }
}
