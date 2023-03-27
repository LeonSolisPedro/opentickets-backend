using ApplicationCore.Helpers;
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
    public class ComputadoraService : CRUD<Computadora>, IComputadoraService
    {

        private readonly IRepository _repo;

        public ComputadoraService(IRepository repo): base(repo)
        {
            _repo = repo;
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
