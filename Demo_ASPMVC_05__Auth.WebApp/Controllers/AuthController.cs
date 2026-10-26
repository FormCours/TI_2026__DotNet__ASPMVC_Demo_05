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
            await CreateMemberSessionAsync(member);

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterForm data)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            MemberModel memberToRegister = new MemberModel
            {
                Email = data.Email,
                Role = "User"
            };
            MemberModel? member = authService.Register(memberToRegister, data.Pwd);
            if(member is null)
            {
                // TODO Prévoir une redirection ou un message si le compte existe déjà
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite !");
                return View();
            }

            await CreateMemberSessionAsync(member);

            return RedirectToAction("Index", "Home");
        }

        private async Task CreateMemberSessionAsync(MemberModel member)
        {
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
        }
    }
}
