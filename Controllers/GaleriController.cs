using Microsoft.AspNetCore.Mvc;
using NotDefteri.Models;
using NotDefteri.Data;
using System.Linq;

namespace NotDefteri.Controllers
{
    public class GaleriController : Controller
    {
        public IActionResult Index()
        {
            var resimler = ResimVeritabani.ResimleriGetir();
            return View(resimler);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Resim resim, IFormFile file)
        {
            var resimler = ResimVeritabani.ResimleriGetir();

            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                resim.ResimUrl = "/images/" + fileName;
            }

            resim.Id = resimler.Count == 0 ? 1 : resimler.Max(x => x.Id) + 1;

            resimler.Add(resim);
            ResimVeritabani.Kaydet(resimler);

            return RedirectToAction("Index");
        }

        public IActionResult Duzenle(int id)
        {
            var resimler = ResimVeritabani.ResimleriGetir();
            var resim = resimler.FirstOrDefault(x => x.Id == id);
            if (resim == null)
                return NotFound();

            return View(resim);
        }

        [HttpPost]
public IActionResult Duzenle(Resim gelenResim, IFormFile file)
{
    var resimler = ResimVeritabani.ResimleriGetir();
    var resim = resimler.FirstOrDefault(x => x.Id == gelenResim.Id);

    if (resim == null)
        return NotFound();

    resim.Ad = gelenResim.Ad;
    resim.Aciklama = gelenResim.Aciklama;

    if (file != null && file.Length > 0)
    {
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        // Eski dosyayı sil
        if (!string.IsNullOrEmpty(resim.ResimUrl))
        {
            var eskiDosya = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", resim.ResimUrl.TrimStart('/'));
            if (System.IO.File.Exists(eskiDosya))
            {
                System.IO.File.Delete(eskiDosya);
            }
        }

        // Yeni dosyayı kaydet
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploads, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        resim.ResimUrl = "/images/" + fileName;
    }

    ResimVeritabani.Kaydet(resimler);

    return RedirectToAction("Index");
}

        public IActionResult Sil(int id)
        {
            var resimler = ResimVeritabani.ResimleriGetir();
            var resim = resimler.FirstOrDefault(x => x.Id == id);

            if (resim != null)
            {
            
                if (!string.IsNullOrEmpty(resim.ResimUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", resim.ResimUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                resimler.Remove(resim);
                ResimVeritabani.Kaydet(resimler);
            }

            return RedirectToAction("Index");
        }
    }
}