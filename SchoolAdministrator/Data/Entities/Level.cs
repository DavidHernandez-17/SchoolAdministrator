using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Data.Entities
{
    public class Level
    {

        public int Id { get; set; }

        [Display(Name = "Nivel")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Tipo de Nivel")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LevelType { get; set; }


        //public Institution Institution { get; set; }

        //public ICollection<Subject> Subjects { get; set; }

        //[Display(Name = "Niveles/Instituciones")]
        //public int SubjectsNumber => Subjects == null ? 0 : Subjects.Count;

    }
}