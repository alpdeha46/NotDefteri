namespace NotDefteri.Models

{
    public class Not
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
       public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public bool OnemliMi { get; set; }
        public string Kullanici { get; set; }
    }
}