using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Context
{
    public static class OpenTicketsSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OpenTicketsContext(serviceProvider.GetRequiredService<DbContextOptions<OpenTicketsContext>>()))
            {
                // Si hay computadoras, ya no continues
                if (context.Computadoras.Any())
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

                context.Computadoras.Add(computadora1);
                context.SaveChanges();

                var empleado1 = new Empleado
                {
                    IdComputadora = computadora1.Id,
                    NombreEmpleado = "Juan Pancho",
                    NombreDepartamento = "Ventas"
                };


                context.Empleados.Add(empleado1);
                context.SaveChanges();
            }
        }
    }
}
