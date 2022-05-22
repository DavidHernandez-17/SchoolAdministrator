using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Common;
using SchoolAdministrator.Data;
using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Enums;
using SchoolAdministrator.Helpers;
using SchoolAdministrator.Models;
using Vereyon.Web;

namespace SchoolAdministrator.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public AccountController(IUserHelper userHelper, DataContext Context, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper, IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _context = Context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }

        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new()
            {
                DocumentType = user.DocumentType,
                Document = user.Document,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                PhoneNumber = user.PhoneNumber,
                ImageId = user.ImageId,
                Levels = await _combosHelper.GetComboLevelsAsync(user.Institution.Id),
                Institions = await _combosHelper.GetComboInstitutionsAsync(),
                Id = user.Id,
            };
            return View(model);
        }

        public async Task<IActionResult> Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                Institions = await _combosHelper.GetComboInstitutionsAsync(),
                Levels = await _combosHelper.GetComboLevelsAsync(0),
                Inscriptions = await _combosHelper.GetComboInscriptionsAsync(),
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
                    _flashMessage.Danger("Este correo ya está siendo usado.");
                    model.Institions = await _combosHelper.GetComboInstitutionsAsync();
    
                    return View(model);
                }

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(
                    $"{model.FirstName} {model.LastName}",
                    model.Username,
                    "School Administrator - Confirmación de Email",
                    $"<h1>School Administrator - Confirmación de Email</h1>" +
                        $"Para habilitar el usuario por favor hacer click en el siguiente link:, " +
                        $"<hr/><br/><p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                if (response.IsSuccess)
                {
                    _flashMessage.Info("Usuario registrado. Para poder ingresar al sistema, siga las instrucciones que han sido enviadas a su correo.");
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            model.Institions = await _combosHelper.GetComboInstitutionsAsync();
            model.Levels = await _combosHelper.GetComboLevelsAsync(model.Institution);
            model.Inscriptions = await _combosHelper.GetComboInscriptionsAsync();

            return View(model);
        }

        public JsonResult GetLevels(int institutionId)
        {
            Institution institution = _context.Institutions
                .Include(c => c.Levels)
                .FirstOrDefault(c => c.Id == institutionId);
            if (institution == null)
            {
                return null;
            }

            return Json(institution.Levels.OrderBy(d => d.Name));
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldPassword == model.NewPassword)
                {
                   _flashMessage.Info("Debes ingresar una contraseña diferente.");
                    return View(model);
                }

                User? user = await _userHelper.GetUserAsync(User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }


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
                else if (result.IsNotAllowed)
                {
                    _flashMessage.Info("El usuario no ha sido habilitado, debes de seguir las instrucciones enviadas al correo para poder habilitarlo.");
                }
                else
                {
                    _flashMessage.Danger("Email o contraseña incorrectos.");
                }
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

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user == null)
                {
                    _flashMessage.Danger("El email no corresponde a ningún usuario registrado.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                Response respose = _mailHelper.SendMail(
                    $"{user.FullName}",
                    model.Email,
                    "School Administrator - Recuperación de Contraseña",
                    $"<h1>SchoolAdministrator - Recuperación de Contraseña</h1>" +
                    $"Para recuperar la contraseña haga click en el siguiente enlace:" +
                    $"<p><a href = \"{link}\">Reset Password</a></p>");

                if (respose.IsSuccess)
                {
                    _flashMessage.Confirmation("Las instrucciones para recuperar la contraseña han sido enviadas a su correo.");
                }
                else
                {
                    _flashMessage.Danger(respose.Message);
                }

                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    _flashMessage.Confirmation("Contraseña cambiada con éxito.");
                    return RedirectToAction(nameof(Login));
                }

                _flashMessage.Danger("Error cambiando la contraseña.");
                return View(model);
            }

            _flashMessage.Danger("Usuario no encontrado.");
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }



    }

}
