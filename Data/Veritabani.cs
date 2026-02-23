using NotDefteri.Models;
using System.Collections.Generic;

namespace NotDefteri.Data
{
    public static class Veritabani
    {
        public static List<Not> Notlar = new List<Not>()
        {
            new Not { Id = 1, Baslik = "İlk Not", Icerik = "Bu bir örnek nottur." },
            new Not { Id = 2, Baslik = "MVC", Icerik = "MVC öğreniyorum." }
        };
    }
}