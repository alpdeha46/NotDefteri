using NotDefteri.Models;
using System.Text.Json;

namespace NotDefteri.Data
{
    public static class ResimVeritabani
    {
        private static string dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "resimler.json");

        public static List<Resim> ResimleriGetir()
        {
            if (!File.Exists(dosyaYolu))
                return new List<Resim>();

            var json = File.ReadAllText(dosyaYolu);
            return JsonSerializer.Deserialize<List<Resim>>(json) ?? new List<Resim>();
        }

        public static void Kaydet(List<Resim> resimler)
        {
            var json = JsonSerializer.Serialize(resimler, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(dosyaYolu, json);
        }
    }
}