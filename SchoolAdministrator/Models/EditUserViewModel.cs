using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Tipo Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string DocumentType { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Edad")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Age { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7213/images/noimage.png"
            : $"https://schooladministrator.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }

        //____________________________________________________________________________________________________________


        [Display(Name = "Institución")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una institución.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Institution { get; set; }

        public IEnumerable<SelectListItem> Institions { get; set; }

        
        [Display(Name = "Nivel")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Level")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Level { get; set; }

        public IEnumerable<SelectListItem> Levels { get; set; }


        [Display(Name = "Periodo Académico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Inscription { get; set; }

        public IEnumerable<SelectListItem> Inscriptions { get; set; }

    }

}
