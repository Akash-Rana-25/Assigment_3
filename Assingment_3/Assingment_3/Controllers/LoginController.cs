using Assingment_3.Contex;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Assingment_3.DTO;

namespace Assingment_3.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Email == model.Email);

                if (customer != null && BCrypt.Net.BCrypt.Verify(model.Password, customer.Password))
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                        new Claim(ClaimTypes.Name, customer.FirstName + " " + customer.LastName),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "MyProfile");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return RedirectToAction("Register", "Registration");
        }
    }
}
