using Assingment_3.Contex;
using Assingment_3.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assingment_3.Controllers
{

    public class MyProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == int.Parse(userId));

            if (customer == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var model = new CustomerProfileUpdateDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ContactNo = customer.ContactNo,
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email,
                AddedOn = customer.DateAdded,
                UpdatedOn = customer.LastUpdated
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CustomerProfileUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var customer = _context.Customers.SingleOrDefault(c => c.Id == int.Parse(userId));

                if (customer != null)
                {
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    customer.ContactNo = model.ContactNo;
                    customer.DateOfBirth = model.DateOfBirth;
                    customer.Email = model.Email;

                    // Update password if provided
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        // Hash the new password before storing it
                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                        customer.Password = hashedPassword;
                    }

                    // Update last updated timestamp
                    customer.LastUpdated = DateTime.Now;

                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Your details are updated successfully.";
                    return RedirectToAction("Index");
                }
            }

            return View("Index", model);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "You Logged out successfully.";
            return RedirectToAction("Login", "Login");
        }
    }
}
