using System.Reflection;
using Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Computadora> Computadoras => Set<Computadora>();
  public DbSet<Empleado> Empleados => Set<Empleado>();
  public DbSet<Ticket> Tickets => Set<Ticket>();
  public DbSet<Solucion> Soluciones => Set<Solucion>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
