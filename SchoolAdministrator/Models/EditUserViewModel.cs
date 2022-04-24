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
            ? $"https://localhost:7057/images/noimage.png"
            : $"https://shoppingprep.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        //____________________________________________________________________________________________________________


        [Display(Name = "Institución")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una institución.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Institution { get; set; }
        //TODO: Combo institution
        //public IEnumerable<SelectListItem> Institions { get; set; }


        [Display(Name = "Level")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Level")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Level { get; set; }
        //TODO: Combo States
        //public IEnumerable<SelectListItem> States { get; set; }


        [Display(Name = "Periodo Académico")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una ciudad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Inscription { get; set; }

        //TODO: Combo Cities
        //public IEnumerable<SelectListItem> Cities { get; set; }
    }

}
