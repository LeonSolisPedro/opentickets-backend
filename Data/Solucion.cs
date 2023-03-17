using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opentickets_backend.Data
{
	public class Solucion
	{
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ticket")]
        public int IdTicket { get; set; }
        public Ticket? Ticket { get; set; }

        public bool ModificoCompu { get; set; }

        [Required]
        public string SolucionCampo { get; set; } = "";
    }
}

