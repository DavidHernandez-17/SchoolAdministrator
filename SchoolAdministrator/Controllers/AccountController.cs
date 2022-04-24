using Microsoft.AspNetCore.Mvc;
using SchoolAdministrator.Data;
using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Enums;
using SchoolAdministrator.Helpers;
using SchoolAdministrator.Models;

namespace SchoolAdministrator.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;

        public AccountController(IUserHelper userHelper, DataContext Context, IBlobHelper blobHelper)
        {
            _userHelper = userHelper;
            _context = Context;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                UserType = UserType.User,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.AddUserAsync(model, imageId);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    return View(model);
                }

                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };

                var result2 = await _userHelper.LoginAsync(loginViewModel);

                if (result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }


        //public JsonResult GetStates(int countryId)
        //{
        //    Country country = _context.Countries
        //        .Include(c => c.States)
        //        .FirstOrDefault(c => c.Id == countryId);
        //    if (country == null)
        //    {
        //        return null;
        //    }

        //    return Json(country.States.OrderBy(d => d.Name));
        //}

        //public JsonResult GetCities(int stateId)
        //{
        //    State state = _context.States
        //        .Include(s => s.Cities)
        //        .FirstOrDefault(s => s.Id == stateId);
        //    if (state == null)
        //    {
        //        return null;
        //    }

        //    return Json(state.Cities.OrderBy(c => c.Name));
        //}

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

    }

}
