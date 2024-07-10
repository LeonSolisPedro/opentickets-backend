using Core.Entites;

namespace Core.Repositories;

public interface IComputadoraRepository
{
    Task<Computadora> GetPrimerCompu();
}
