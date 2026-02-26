using NotDefteri.Models;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NotDefteri.Data
{
    public static class Veritabani
    {
        
        private static readonly string NotlarDosya = "notlar.json";
        private static readonly string KullanicilarDosya = "kullanicilar.json";

        public static List<Not> Notlar { get; set; } = new List<Not>();
        public static List<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();

        static Veritabani()
        {
            Yükle();
        }

        public static void Yükle()
        {
            if (File.Exists(NotlarDosya))
            {
                Notlar = JsonConvert.DeserializeObject<List<Not>>(File.ReadAllText(NotlarDosya)) ?? new List<Not>();
            }

            if (File.Exists(KullanicilarDosya))
            {
                Kullanicilar = JsonConvert.DeserializeObject<List<Kullanici>>(File.ReadAllText(KullanicilarDosya)) ?? new List<Kullanici>();
            }
            else
            {
                
                Kullanicilar.Add(new Kullanici
                {
                    KullaniciAdi = "admin",
                    Sifre = "1234",
                    Ayarlar = new Ayarlar() { DarkMode = true, NotlariTersSirala = false, NotTarihiGoster = true }
                });
                KaydetKullanicilar();
            }
        }

        public static void KaydetNotlar()
        {
            File.WriteAllText(NotlarDosya, JsonConvert.SerializeObject(Notlar, Formatting.Indented));
        }

        public static void KaydetKullanicilar()
        {
            File.WriteAllText(KullanicilarDosya, JsonConvert.SerializeObject(Kullanicilar, Formatting.Indented));
        }
    }
}