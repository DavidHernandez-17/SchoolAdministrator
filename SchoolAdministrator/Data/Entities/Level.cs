using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrator.Data.Entities
{
    public class Level
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

        [JsonIgnore]
        public Institution Institution { get; set; }

        //[Display(Name = "Niveles/Instituciones")]
        //public int SubjectsNumber => Subjects == null ? 0 : Subjects.Count;

    }
}