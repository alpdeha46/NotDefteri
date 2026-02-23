using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NotDefteri.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string kullanici, string sifre)
        {
            if (kullanici == "admin" && sifre == "1234")
            {
                HttpContext.Session.SetString("kullanici", kullanici);

                return RedirectToAction("Ekle", "Note");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}