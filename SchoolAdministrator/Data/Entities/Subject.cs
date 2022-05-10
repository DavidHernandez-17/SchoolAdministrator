using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Data.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Categoría")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CategorySubject { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
