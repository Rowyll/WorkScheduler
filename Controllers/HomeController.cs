using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkScheduler.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WorkScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authorization()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(string phoneNumber)
        {
            using(WorkSchedulerContext db = new WorkSchedulerContext())
            {
                Worker worker = (from c in db.Workers where c.PhoneNumber == phoneNumber select c).FirstOrDefault();
                if (worker != null)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, worker.WorkerId.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "worker")};
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    
                    return RedirectToAction("LoadSchedule");
                }
                else
                {
                    ViewBag.Enter = "Неверные данные для входа";
                    return View();
                }
            }
        }

        [Authorize]
        public IActionResult LoadSchedule()
        {
            var worker = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return View("Index", worker.Value);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var worker = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return View("Profile", worker.Value);
        }

        public RedirectToActionResult LeaveAccount()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Authorization");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}