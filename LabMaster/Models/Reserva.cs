using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabMaster.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaID { get; set; }

        public string UsuarioID { get; set; }

       
        [Required]
        public int LabID { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        // Asegúrate de que se llame FechaReserva como pide el controlador
        public DateTime FechaReserva { get; set; }

        [Required]
        [Display(Name = "Hora de Inicio")]
        // Cambiamos a string para evitar el error de conversión de TimeSpan
        public string HoraInicio { get; set; }

        [Display(Name = "Motivo de la práctica")]
        public string Motivo { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual ApplicationUser Usuario { get; set; }

        public virtual Laboratorio Laboratorio { get; set; }
    }
}