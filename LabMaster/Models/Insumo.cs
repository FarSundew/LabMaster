using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabMaster.Models
{
    public class Insumo
    {
        [Key]
        public int InsumoID { get; set; }

        [Required]
        [Display(Name = "Material/Equipo")]
        public string NombreInsumo { get; set; }

        [Required]
        public int Stock { get; set; }

        // Relación con la Carrera
        public int CarreraID { get; set; }
        public virtual Carrera Carrera { get; set; }

    }
}