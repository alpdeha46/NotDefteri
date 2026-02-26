using Microsoft.AspNetCore.Mvc;
using NotDefteri.Data;

namespace NotDefteri.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string kullanici, string sifre)
        {
            Veritabani.Yükle();
            var user = Veritabani.Kullanicilar
                        .FirstOrDefault(u => u.KullaniciAdi == kullanici);

            if (user != null && user.Sifre == sifre)
            {
                HttpContext.Session.SetString("kullanici", kullanici);
                return RedirectToAction("Index", "Anasayfa"); 
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}