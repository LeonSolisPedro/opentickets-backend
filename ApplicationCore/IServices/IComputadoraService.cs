using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using ApplicationCore.IServices.Generic;
using Infrastructure.Models;

namespace ApplicationCore.IServices
{
    public interface IComputadoraService : IGenericService<Computadora>
    {
        Task<List<Computadora>> GetComputadorasDropdown(string? empleados);
        Task<string> SayHi();
    }
}
