using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
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

    public class OpenTicketsContextFactory : IDesignTimeDbContextFactory<OpenTicketsContext>
    {
        public OpenTicketsContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OpenTicketsContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new OpenTicketsContext(optionsBuilder.Options);
        }
    }
}
