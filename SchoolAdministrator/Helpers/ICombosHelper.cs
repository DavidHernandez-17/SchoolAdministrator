using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolAdministrator.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboInstitutionsAsync();

        Task<IEnumerable<SelectListItem>> GetComboLevelsAsync(int institutionId);

    }
}
