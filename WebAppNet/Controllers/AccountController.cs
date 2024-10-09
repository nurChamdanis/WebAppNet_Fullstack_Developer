using Microsoft.AspNetCore.Mvc;
using WebAppNet.Interface;
using WebAppNet.Models;

namespace WebAppNet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _repo; // Inject your repository

        public AccountController(IUserRepo repo)
        {
            _repo = repo; // Initialize the repository
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());  
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _repo.Authenticate(model.Username, model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserSession", model.Username); 
                    return RedirectToAction("Index", "Home");  
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password. \n if user account not exists.");
                    return RedirectToAction("Register", "Account"); // Redirect to home
                }
            }

            return View("Login", model); // Return to the login view with validation errors
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel()); // Return an empty model for the registration view
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Assuming you have a method to register the user
                bool registrationSuccess = _repo.Register(model.Username, model.Password);
                if (registrationSuccess)
                {
                    HttpContext.Session.SetString("UserSession", model.Username); // Store username in session
                    return RedirectToAction("Index", "Home"); // Redirect to home
                }
                ModelState.AddModelError("", "Registration failed.");
            }

            return View(model); // Return to the registration view with validation errors
        }
    }
}
