#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data;
using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Helpers;
using SchoolAdministrator.Models;
using Vereyon.Web;
using static SchoolAdministrator.Helpers.ModalHelper;

namespace SchoolAdministrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstitutionsController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public InstitutionsController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        // GET: Institutions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutions
                .Include(i => i.Levels)
                .ToListAsync());
        }

        // GET: Institutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .Include(i => i.Levels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }


        public async Task<IActionResult> DetailsLevel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Level level = await _context.Levels
                .Include(i => i.Institution)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // GET: Institutions/Create
        public IActionResult Create()
        {
            Institution institution = new() { Levels = new List<Level>() };
            return View(institution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Institution institution)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(institution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Info("Ya existe una Institución con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(institution);

        }

        public async Task<IActionResult> AddLevel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Institution institution = await _context.Institutions.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }

            LevelViewModel model = new()
            {
                InstitutionId = institution.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLevel(LevelViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Level level = new()
                    {
                        Institution = await _context.Institutions.FindAsync(model.InstitutionId),
                        Name = model.Name,
                        Type = model.Type,
                    };
                    _context.Add(level);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Info("Ya existe un Nivel con el mismo nombre, en esta Institución.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);

        }

        // GET: Institutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .Include(i => i.Levels)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (institution == null)
            {
                return NotFound();
            }
            return View(institution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Institution institution)
        {
            if (id != institution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Info("Ya existe una institución con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(institution);

        }

        public async Task<IActionResult> EditLevel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Level level = await _context.Levels
                .Include(l => l.Institution)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            LevelViewModel model = new()
            {
                InstitutionId = level.Institution.Id,
                Id = level.Id,
                Name = level.Name,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLevel(int id, LevelViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Level level = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Type = model.Type,
                    };
                    _context.Update(level);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.InstitutionId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Info("Ya existe un Nivel con el mismo nombre, en esta Institución.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);

        }


        private bool InstitutionExists(int id)
        {
            return _context.Institutions.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteLevel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Level level = await _context.Levels
                .Include(l => l.Institution)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }


        [HttpPost, ActionName("DeleteLevel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLevelConfirmed(int id)
        {
            Level level = await _context.Levels
                .Include(l => l.Institution)
                .FirstOrDefaultAsync(l => l.Id == id);
            _context.Levels.Remove(level);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = level.Institution.Id });
        }


        [NoDirectAccess]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Institution institution = await _context.Institutions.FirstOrDefaultAsync(c => c.Id == id);
            if (institution == null)
            {
                return NotFound();
            }

            try
            {
                _context.Institutions.Remove(institution);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar la institución porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Index));
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Institution());
            }
            else
            {
                Institution institution = await _context.Institutions.FindAsync(id);
                if (institution == null)
                {
                    return NotFound();
                }

                return View(institution);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Institution institution)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0) //Insert
                    {
                        _context.Add(institution);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro creado.");
                    }
                    else //Update
                    {
                        _context.Update(institution);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro actualizado.");
                    }
                    return Json(new
                    {
                        isValid = true,
                        html = ModalHelper.RenderRazorViewToString(
                            this,
                            "_ViewAll",
                            _context.Institutions
                                .Include(c => c.Levels)
                                .ToList())
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe una institución con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", institution) });
        }


    }



}
