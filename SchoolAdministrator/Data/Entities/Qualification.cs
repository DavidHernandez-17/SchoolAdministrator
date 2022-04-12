using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Data.Entities
{
    public class Qualification
    {
        public int Id { get; set; }

        [Display(Name = "Calificación")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float qualification { get; set; }

        //public ICollection<Subject> Subjects { get; set; }
        //public User user { get; set; }
        //public User Level { get; set; }
        //public User LevelInstitution { get; set;}
    }
}
