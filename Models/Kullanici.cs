namespace NotDefteri.Models
{
    public class Kullanici
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; } 
        public Ayarlar Ayarlar { get; set; } = new Ayarlar();
    }

    public class Ayarlar
    {
        public bool DarkMode { get; set; } = true;
        public bool NotTarihiGoster { get; set; } = true;
        public bool NotlariTersSirala { get; set; } = false;
    }
}

