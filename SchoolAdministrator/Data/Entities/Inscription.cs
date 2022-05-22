using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Data.Entities
{
    public class Inscription
    {
        public int Id { get; set; }

        [Display(Name = "Perido académico")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PeriodAcedemic { get; set; }
    }
}
