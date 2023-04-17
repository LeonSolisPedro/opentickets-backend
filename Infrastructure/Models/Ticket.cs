using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Computadora")]
        public int IdComputadora { get; set; }
        public Computadora? Computadora { get; set; }

        public Prioridad Prioridad { get; set; }

        [Required]
        public string DescripcionProblema { get; set; } = "";

        //Despues, implementar quien era el empleado con idEmpleado

        public Solucion? Solucion { get; set; }

    }
}

