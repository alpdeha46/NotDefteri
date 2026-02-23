using Microsoft.AspNetCore.Mvc;

namespace NotDefteri.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string kullanici)
        {
            HttpContext.Session.SetString("kullanici", kullanici);
            return RedirectToAction("Index", "Note");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}