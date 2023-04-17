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
