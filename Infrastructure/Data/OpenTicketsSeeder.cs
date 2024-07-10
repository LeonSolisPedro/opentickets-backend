

using Core.Entites;
using static Core.Dto.Enums;

namespace Infrastructure.Data;

public class OpenTicketsSeeder
{
  public static async Task SeedAsync(OpenTicketsContext _context)
  {
    // Si hay computadoras, ya no continues
    if (_context.Computadoras.Any())
      return;

    var computadora1 = new Computadora
    {
      TipoComputadora = TipoComputadora.LAPTOP,
      MarcaModel = "Computadora 1",
      NumeroSerie = "2525",
      Procesador = "i5 5200",
      RAM = "8 GB",
      Disco = "1TB",
      SistemaOperativo = "Windows 11",
    };

    var computadora2 = new Computadora
    {
      TipoComputadora = TipoComputadora.ESCRITORIO,
      MarcaModel = "Computadora 2",
      NumeroSerie = "2626",
      Procesador = "M1",
      RAM = "16 GB",
      Disco = "256 GB",
      SistemaOperativo = "macOS Ventura",
    };

    var computadora3 = new Computadora
    {
      TipoComputadora = TipoComputadora.LAPTOP,
      MarcaModel = "Computadora 3",
      NumeroSerie = "2727",
      Procesador = "i5 5600",
      RAM = "16 GB",
      Disco = "256 GB",
      SistemaOperativo = "Ubuntu 20",
    };

    _context.Computadoras.Add(computadora1);
    _context.Computadoras.Add(computadora2);
    _context.Computadoras.Add(computadora3);

    var empleado1 = new Empleado
    {
      Computadora = computadora1,
      NombreEmpleado = "Juan Pancho",
      NombreDepartamento = "Ventas"
    };


    _context.Empleados.Add(empleado1);

    var ticket1 = new Ticket
    {
      Computadora = computadora1,
      Prioridad = Prioridad.BAJA,
      DescripcionProblema = "Instalar office en la computadora del empleado"
    };

    _context.Tickets.Add(ticket1);
    await _context.SaveChangesAsync();
  }
}
