using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project_18.Data;
using project_18.Models;
using System.Linq;

namespace project_18.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext context { get; set; }

        public AccountController(AppDbContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(user);
                }

                user.Role = "Customer"; // Force registered users as Customers
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both username and password.";
                return View();
            }

            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("FullName", user.FullName);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error = "Please fill in all fields.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "New password and confirmation do not match.";
                return View();
            }

            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            if (user.Password != currentPassword)
            {
                ViewBag.Error = "Current password is incorrect.";
                return View();
            }

            user.Password = newPassword;
            context.SaveChanges();

            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Password changed successfully. Please log in with your new password.";
            return RedirectToAction("Login");
        }
    }
}
