using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web.Helpers;
using WebAppNet.Interface; 
using WebAppNet.Models;
using WebAppNet.Repository;

namespace WebAppNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo _repoUser;
        private readonly IHobyRepo _repoHobby;

        public HomeController(ILogger<HomeController> logger, IUserRepo users, IHobyRepo hobby)
        {
            _logger = logger;
            _repoUser = users;
            _repoHobby = hobby;
        }

        public IActionResult Index()
        {
            // Check if the session variable "UserSession" is empty
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                // Redirect to the login page if the session is empty and not already on the Login page
                if (HttpContext.Request.Path != "/Account/Login")
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View();
        }


        #region saveGenerateData
        [HttpPost]
        [Route("Home/saveGenerateData")]
        public JsonResult saveGenerateData([FromBody] List<HobbyGenerate> hobby) // Ensure HobbyGenerate is the correct model
        {
            if (hobby == null || hobby.Count == 0)
            {
                return Json(new { status = 400, message = "No data received." });
            }

            int savedCount = 0; // Counter for successfully saved users

            foreach (var user in hobby)
            {
                // Call the saveHobby method from your repository
                bool isSaved = _repoHobby.saveHobby(user.Name, user.Gender, user.Hobby_detail, user.Age.ToString());

                if (isSaved)
                {
                    savedCount++;
                }
            }

            var response = new
            {
                status = 200,
                count = savedCount,
                message = "Data saved successfully!"
            };
            return Json(response);
        }
        #endregion



        #region getUsers
        [HttpPost]
        [Route("Home/getUsers")]
        public JsonResult getUsers(string email, string password)
        {
            var result = _repoUser.Authenticate(email, password);
            var response = new
            {
                status = 200,
                data = result,
                count = result.Count,
                message = "Data retrieved successfully!"
            };
            return Json(response);
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult runGenerateData()
        {
            var result = _repoHobby.generateHobby();
            var response = new
            {
                status = 200,
                data = result,
                count = 0,
                message = "get data successfully!"
            };
            return Json(response); // Updated method without JsonRequestBehavior
        }
    }
}
