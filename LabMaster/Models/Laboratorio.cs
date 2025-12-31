using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabMaster.Models
{
    public class Laboratorio
    {
        [Key]
        public int LabID { get; set; }
        [Required]
        public string NombreLab { get; set; }
        public int Capacidad { get; set; }

        public int CarreraID { get; set; }
        [ForeignKey("CarreraID")]
        public virtual Carrera Carrera { get; set; }
    }
}