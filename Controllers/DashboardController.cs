using Microsoft.AspNetCore.Mvc;
using NotDefteri.Data;
using System.Linq;

namespace NotDefteri.Controllers
{
    public class DashboardController : Controller
    {
        private string KullaniciAdi => HttpContext.Session.GetString("kullanici");

        private IActionResult LoginKontrol()
        {
            if (string.IsNullOrEmpty(KullaniciAdi))
                return RedirectToAction("Index", "Home");
            return null;
        }

        public IActionResult Index()
        {
            var redirect = LoginKontrol();
            if (redirect != null) return redirect;

            var notlar = Veritabani.Notlar
                .Where(n => n.Kullanici == KullaniciAdi)
                .ToList();

            var resimler = ResimVeritabani.ResimleriGetir();

            ViewBag.ToplamNot = notlar.Count;
            ViewBag.ToplamOnemli = notlar.Count(n => n.OnemliMi);
            ViewBag.ToplamResim = resimler.Count;

            return View();
        }
    }
}