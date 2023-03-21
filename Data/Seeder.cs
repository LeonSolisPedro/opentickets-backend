﻿using System;
using Microsoft.EntityFrameworkCore;
using static opentickets_backend.Data.Enums;

namespace opentickets_backend.Data
{
    public static class Seeder
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

