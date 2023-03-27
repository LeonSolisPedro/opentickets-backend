using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using Infrastructure.Models;

namespace ApplicationCore.IServices
{
    public interface IComputadoraService : ICRUD<Computadora>
    {
        Task<List<Computadora>> GetComputadorasDropdown(string? empleados);
        Task<string> SayHi();
    }
}
