using System;
using Microsoft.EntityFrameworkCore;

namespace opentickets_backend.Data
{
	public class OpenTicketsContext : DbContext
	{
        public OpenTicketsContext(DbContextOptions<OpenTicketsContext> options)
        : base(options)
        {
        }

        public DbSet<Computadora> Computadoras => Set<Computadora>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Solucion> Soluciones => Set<Solucion>();


    }
}

