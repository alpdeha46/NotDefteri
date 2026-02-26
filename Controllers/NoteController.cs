using Microsoft.AspNetCore.Mvc;
using NotDefteri.Models;
using NotDefteri.Data;
using System.Linq;

namespace NotDefteri.Controllers
{
    public class NoteController : Controller
    {
        private string? KullaniciAdi => HttpContext.Session.GetString("kullanici");

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
                .Where(n => n.Kullanici == KullaniciAdi);

            var ayar = Veritabani.Kullanicilar.First(u => u.KullaniciAdi == KullaniciAdi).Ayarlar;

            if (ayar.NotlariTersSirala)
                notlar = notlar.OrderByDescending(n => n.OlusturmaTarihi);

            return View(notlar.ToList());
        }

        public IActionResult Ekle()
        {
            if (LoginKontrol() != null) return LoginKontrol();
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Not not)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            not.Id = Veritabani.Notlar.Count > 0 ? Veritabani.Notlar.Max(n => n.Id) + 1 : 1;
            not.Kullanici = KullaniciAdi;
            not.OlusturmaTarihi = DateTime.Now;

            Veritabani.Notlar.Add(not);
            Veritabani.KaydetNotlar(); 

            return RedirectToAction("Index");
        }

        public IActionResult Duzenle(int id)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            var not = Veritabani.Notlar.FirstOrDefault(n => n.Id == id);
            return View(not);
        }

        [HttpPost]
        public IActionResult Duzenle(Not gelenNot)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            var not = Veritabani.Notlar.FirstOrDefault(n => n.Id == gelenNot.Id);
            if (not != null)
            {
                not.Baslik = gelenNot.Baslik;
                not.Icerik = gelenNot.Icerik;
                not.OnemliMi = gelenNot.OnemliMi;

                Veritabani.KaydetNotlar(); 
            }

            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            var not = Veritabani.Notlar.FirstOrDefault(n => n.Id == id);
            if (not != null)
            {
                Veritabani.Notlar.Remove(not);
                Veritabani.KaydetNotlar();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Ayarlar()
        {
            if (LoginKontrol() != null) return LoginKontrol();
            return View();
        }

        [HttpPost]
        public IActionResult SifreDegistir(string eskiSifre, string yeniSifre)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            var kullanici = Veritabani.Kullanicilar.FirstOrDefault(u => u.KullaniciAdi == KullaniciAdi);

            if (kullanici != null && kullanici.Sifre == eskiSifre)
            {
                kullanici.Sifre = yeniSifre;
                Veritabani.KaydetKullanicilar(); // Kalıcı
                TempData["Mesaj"] = "Şifre başarıyla değiştirildi!";
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
            if (LoginKontrol() != null) return LoginKontrol();

            Veritabani.Notlar.RemoveAll(n => n.Kullanici == KullaniciAdi);
            Veritabani.KaydetNotlar();
            TempData["Mesaj"] = "Tüm notlar silindi!";
            return RedirectToAction("Ayarlar");
        }
        [HttpPost]
        public IActionResult TumGorselleriSil()
        {
        var gorselKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (Directory.Exists(gorselKlasoru))
        {
            Directory.Delete(gorselKlasoru, true);
            Directory.CreateDirectory(gorselKlasoru);
        }

   
        var tumResimler = ResimVeritabani.ResimleriGetir();
        tumResimler.RemoveRange(0, tumResimler.Count);
        ResimVeritabani.Kaydet(tumResimler);

        TempData["Mesaj"] = "Tüm görseller silindi.";
        return RedirectToAction("Index"); 
    }
       
        [HttpPost]
        public IActionResult AyarGuncelle(bool NotTarihiGoster, bool NotlariTersSirala, bool DarkMode)
        {
            if (LoginKontrol() != null) return LoginKontrol();

            var kullanici = Veritabani.Kullanicilar.First(u => u.KullaniciAdi == KullaniciAdi);
            kullanici.Ayarlar.NotTarihiGoster = NotTarihiGoster;
            kullanici.Ayarlar.NotlariTersSirala = NotlariTersSirala;
            kullanici.Ayarlar.DarkMode = DarkMode;
            Veritabani.KaydetKullanicilar();

            TempData["Mesaj"] = "Ayarlar kaydedildi!";
            return RedirectToAction("Ayarlar");
        }
    }
}