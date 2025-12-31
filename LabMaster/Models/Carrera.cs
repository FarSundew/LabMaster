using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabMaster.Models
{
    public class Carrera
    {
        [Key]
        public int CarreraID { get; set; }
        [Required]
        [Display(Name = "Nombre de la Carrera")]
        public string NombreCarrera { get; set; }

        public virtual ICollection<Laboratorio> Laboratorios { get; set; }
    }
}