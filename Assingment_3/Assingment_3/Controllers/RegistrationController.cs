using System;
using System.Linq;
using System.Threading.Tasks;
using Assingment_3.Contex;
using Assingment_3.DTO;
using Assingment_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                if (_context.Customers.Any(c => c.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(model);
                }

                // Hash the password before storing it
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ContactNo = model.ContactNo,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    Password = hashedPassword, // Store the hashed password
                    DateAdded = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                TempData["Message"] = "You are registered successfully. You can login with your Email & Password.";
                return RedirectToAction("Login", "Login");
            }

            return View(model); // Validation errors will be displayed on the view
        }
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Login");
        }
    }
   
}
