using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Demo_ASPMVC_05__Auth.BLL.Models;
using Demo_ASPMVC_05__Auth.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Demo_ASPMVC_05__Auth.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginForm data)
        {
            // Traitement du formulaire
            if(!ModelState.IsValid)
            {
                return View();
            }

            MemberModel? member = authService.Login(data.Email, data.Password);
            if(member is null)
            {
                ModelState.AddModelError(string.Empty, "Les crédentials sont invalides");
                return View();
            }

            // Authentification de l'utilisateur
            List<Claim> claims = [
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Role, member.Role),
            ];
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // SignIn
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            // Redirection de l'utilisateur
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Nope()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {           
            // SignOut
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            // Redirection de l'utilisateur
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            throw new NotImplementedException();
        }
    }
}
