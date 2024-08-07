using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entites;

public class Empleado
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Computadora")]
  public int IdComputadora { get; set; }
  public Computadora? Computadora { get; set; }

  [Required]
  public string NombreEmpleado { get; set; } = "";

  [Required]
  public string NombreDepartamento { get; set; } = "";

}
