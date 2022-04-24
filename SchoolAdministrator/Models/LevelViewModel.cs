using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Models
{
    public class LevelViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Nivel")]
        [MaxLength(45, ErrorMessage = "El campo {0} debe tener maximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public String Name { get; set; }


        [Display(Name = "Tipo de Nivel")]
        [MaxLength(45, ErrorMessage = "El campo {0} debe tener maximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public String Type { get; set; }
        
        public int InstitutionId { get; set; }
    }
}
