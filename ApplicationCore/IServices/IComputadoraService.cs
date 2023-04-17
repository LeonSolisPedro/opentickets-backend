using Infrastructure.Models;

namespace ApplicationCore.IServices
{
    public interface IComputadoraService
    {
        Task<List<Computadora>> GetComputadorasDropdown(string? empleados);
        Task<string> SayHi();
    }
}
