using System.ComponentModel.DataAnnotations;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Models
{
    public class Computadora
    {
        [Key]
        public int Id { get; set; }

        public TipoComputadora TipoComputadora { get; set; }

        [Required]
        public string MarcaModel { get; set; } = "";

        public string? NumeroSerie { get; set; }

        [Required]
        public string Procesador { get; set; } = "";

        [Required]
        public string RAM { get; set; } = "";

        [Required]
        public string Disco { get; set; } = "";

        [Required]
        public string SistemaOperativo { get; set; } = "";


        public List<Ticket>? Tickets { get; set; }

        public Empleado? Empleado { get; set; }
    }
}

