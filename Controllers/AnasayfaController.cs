using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NotDefteri.Controllers
{
    public class AnasayfaController : Controller
    {
        public IActionResult Index()
        {
           
            if (HttpContext.Session.GetString("kullanici") == null)
                return RedirectToAction("Index", "Home");

            return View(); 
        }
    }
}