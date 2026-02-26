using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NotDefteri.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
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

                
                return RedirectToAction("Index", "Anasayfa");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }
    }
}