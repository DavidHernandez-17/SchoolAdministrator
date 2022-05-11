using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Models
{
    public class AddSubjectProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Materia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SubjectId { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
    }

}
