using Microsoft.AspNetCore.Mvc;
using NotDefteri.Models;
using NotDefteri.Data;
using System.Linq;

namespace NotDefteri.Controllers
{
    public class NoteController : Controller
    {
        private bool LoginKontrol()
        {
            return HttpContext.Session.GetString("kullanici") != null;
        }

        public IActionResult Index(string ara, bool? onemli)
        {
            if (!LoginKontrol())
                return RedirectToAction("Index", "Home");

            var kullanici = HttpContext.Session.GetString("kullanici");

            var notlar = Veritabani.Notlar
                .Where(x => x.Kullanici == kullanici);

            if (!string.IsNullOrEmpty(ara))
                notlar = notlar.Where(x => x.Baslik.Contains(ara));

            if (onemli == true)
                notlar = notlar.Where(x => x.OnemliMi);

            return View(notlar.ToList());
        }

        public IActionResult Ayarlar()
        {
            if (!LoginKontrol())
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        public IActionResult SifreDegistir(string eskiSifre, string yeniSifre)
        {
            if (!LoginKontrol())
                return RedirectToAction("Index", "Home");

            // Şimdilik sabit şifre sistemi
            if (eskiSifre == "1234")
            {
                // Normalde burada veritabanında güncellenir
                TempData["Mesaj"] = "Şifre başarıyla değiştirildi (demo).";
            }
            else
            {
                TempData["Hata"] = "Eski şifre yanlış!";
            }

            return RedirectToAction("Ayarlar");
        }

        [HttpPost]
        public IActionResult TumNotlariSil()
        {
            if (!LoginKontrol())
                return RedirectToAction("Index", "Home");

            var kullanici = HttpContext.Session.GetString("kullanici");

            Veritabani.Notlar.RemoveAll(x => x.Kullanici == kullanici);

            TempData["Mesaj"] = "Tüm notlar silindi!";
            return RedirectToAction("Ayarlar");
        }
        public IActionResult Ekle()
        {
            if (!LoginKontrol())
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Not not)
        {
            not.Id = Veritabani.Notlar.Count + 1;
            not.Kullanici = HttpContext.Session.GetString("kullanici");
            not.OlusturmaTarihi = DateTime.Now;

            Veritabani.Notlar.Add(not);

            return RedirectToAction("Index");
        }   

        public IActionResult Duzenle(int id)
        {
            var not = Veritabani.Notlar.FirstOrDefault(x => x.Id == id);
            return View(not);
        }

        [HttpPost]
        public IActionResult Duzenle(Not gelenNot)
        {
            var not = Veritabani.Notlar.FirstOrDefault(x => x.Id == gelenNot.Id);

            if (not != null)
            {
                not.Baslik = gelenNot.Baslik;
                not.Icerik = gelenNot.Icerik;
                not.OnemliMi = gelenNot.OnemliMi;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            var not = Veritabani.Notlar.FirstOrDefault(x => x.Id == id);
            if (not != null)
                Veritabani.Notlar.Remove(not);

            return RedirectToAction("Index");
        }
    }

}

