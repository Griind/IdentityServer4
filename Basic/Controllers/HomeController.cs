using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> DoStuff()
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Test").Build();
            var authResult = await _authorizationService.AuthorizeAsync(User, customPolicy);
            if (authResult.Succeeded)
            {
                return View("Index");
            }
            return View("Registration");
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }
        public IActionResult Authenticate()
        {
            var mainClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Vasa"),
                new Claim(ClaimTypes.Email, "Vasa@gmail.com"),
                new Claim("SomeKey", "SomeValue"),
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Vasa Pupkin"),
                new Claim("DrivingRecord", "A+")
        };
            var mainIdentity = new ClaimsIdentity(mainClaims, "someIdentity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "licenseIdentity");

            var userPrincipal = new ClaimsPrincipal(new[] { mainIdentity, licenseIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
