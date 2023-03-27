using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudyLastCore2.Models;
using StudyLastCore2.Repositories;
using System.Security.Claims;

namespace StudyLastCore2.Controllers {
    public class ManagedController : Controller {

        private RepositoryStore repo;

        public ManagedController(RepositoryStore repo) {
            this.repo = repo;
        }

        public IActionResult LogIn() {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password) {
    
            User user = await this.repo.LoginUserAsync(username, password);

            if (user != null) {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name,
                    ClaimTypes.Role
                );

                Claim claimName = new Claim(ClaimTypes.Name, username);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString());
                Claim claimRole = new Claim(ClaimTypes.Role, user.Role);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                return RedirectToAction("Index", "Store");
            } else {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public IActionResult Register() {
            return View();
        }
        
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string name, string password) {
            await this.repo.RegisterUserAsync(name, password);
            return RedirectToAction("Login");
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Store");
        }
    }
}
