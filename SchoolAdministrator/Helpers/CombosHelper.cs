using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data;

namespace SchoolAdministrator.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboLevelsAsync(int institutionId)
        {
            List<SelectListItem> list = await _context.Levels
               .Where(x => x.Institution.Id == institutionId)
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = $"{x.Id}"
               })
               .OrderBy(x => x.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un nivel...]",
                Value = "0"
            });

            return list;

        }


        public async Task<IEnumerable<SelectListItem>> GetComboInstitutionsAsync()
        {
            List<SelectListItem> list = await _context.Institutions.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
             .OrderBy(x => x.Text)
             .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una institución...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSubjectsAsync()
        {
            List<SelectListItem> list = await _context.Subjects.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
             .OrderBy(x => x.Text)
             .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una materia...]",
                Value = "0"
            });

            return list;
        }
    }
}
