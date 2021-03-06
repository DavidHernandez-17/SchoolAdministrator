using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrator.Data.Entities
{
    public class Institution
    {
        public int Id { get; set; }
        
        [Display(Name = "Institución")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Tipo de Institución")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string InstitutionType { get; set; }

        [JsonIgnore]
        public ICollection<Level> Levels { get; set; }

        [Display(Name = "Niveles")]
        public int LevelsNumber => Levels == null ? 0 : Levels.Count;


        //public ICollection<User> Users { get; set; }



    }
}
