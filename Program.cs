using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Calisanlar
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int AtananGorev { get; set; } = 0;
        public int Atamasiniri { get; set; } = 1; 
    }
                                                            //İlk önce çalışanlar ve görev/işlerin olacağı classları oluşturuyorum
    class Gorev
    {
        public string GorevAd { get; set; }
        public int GorevSinir { get; set; }
        public List<Calisanlar> AtananCalisan { get; set; } = new List<Calisanlar>();
        public int Onem { get; set; } 
    }

    static void Main(string[] args)
    {
        List<Gorev> gorevler = new List<Gorev>
        {
            new Gorev { GorevAd = "Araştırma ve Geliştirme", GorevSinir = 3, Onem = 1 },
            new Gorev { GorevAd = "Yönetim", GorevSinir = 4, Onem = 3 },
            new Gorev { GorevAd = "Hata Düzeltme ve kontrol", GorevSinir = 3, Onem = 2 }
        };

        List<Calisanlar> calisanlar = new List<Calisanlar>                           //Eklemeler ve çıkarmalar için önceden belli sayıda görev ve çalışan ekliyorum                     
        {
            new Calisanlar { Adi = "Ahmet", Soyadi = "Demir" },
            new Calisanlar { Adi = "Kemal", Soyadi = "Kaya" },
            new Calisanlar { Adi = "Deniz", Soyadi = "Türk" },
            new Calisanlar { Adi = "Mustafa", Soyadi = "Çelik" }
        };

        while (true)
        {
            Console.WriteLine("\nGörev Takip Sistemi");
            Console.WriteLine("1. Çalışan Ekle");
            Console.WriteLine("2. Çalışan Çıkar");
            Console.WriteLine("3. Görev Ata");
            Console.WriteLine("4. Yeni Görev Ekle");
            Console.WriteLine("5. Görev ve Çalışan Durumlarını Görüntüle");
            Console.WriteLine("0. Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "0") break;

            switch (choice)            //Whileı kullanarak ana bir menü döngüsü oluşturup switch ile seçenekler arasında seçim yapılıp kullanılmaısnı sağlıyorum
            {
                 case "1":
                    CalisanEkle(calisanlar);
                    break;
                case "2":
                    CalisanCikar(calisanlar, gorevler);
                    break;
                case "3":                            
                    GorevAta(calisanlar, gorevler);
                    break;
                case "4":
                    GorevEkle(gorevler);
                    break;
                case "5":
                    DurumuGoster(calisanlar, gorevler);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }
    static void CalisanEkle(List<Calisanlar> calisanlar)
    {
        Console.Write("Çalışan adı: ");
        string adi = Console.ReadLine();
        Console.Write("Çalışan soyadı: ");                                         //Çalışanın isim ve soy ismini alıp calisanlar classına ekliyorum
        string soyadi = Console.ReadLine();                                        //Eklenen çalışanın adını tekrar yazdırarak bilgilendiriyorum

        calisanlar.Add(new Calisanlar { Adi = adi, Soyadi = soyadi });
        Console.WriteLine($"{adi} {soyadi} çalışanı eklendi.");
    }
    static void CalisanCikar(List<Calisanlar> calisanlar, List<Gorev> gorevler)
    {
        Console.Write("Çıkarılacak çalışan adı: ");
        string adi = Console.ReadLine();
        Console.Write("Çıkarılacak çalışan soyadı: ");
        string soyadi = Console.ReadLine();

        Calisanlar calisan = calisanlar.FirstOrDefault(e => e.Adi.Equals(adi, StringComparison.OrdinalIgnoreCase) && e.Soyadi.Equals(soyadi, StringComparison.OrdinalIgnoreCase));

        if (calisan != null)            //Eklenen yada hali hazırda var olan bir çalışanı çıkarıyorum
        {                               //Büyük küçük harf sorunu olmaması için "OrdinalIgnoreCase" kullandım
            foreach (var gorev in gorevler)
            {
                gorev.AtananCalisan.Remove(calisan);
            }
            calisanlar.Remove(calisan);
            Console.WriteLine($"{adi} {soyadi} çalışanı çıkarıldı.");
        }
        else
        {
            Console.WriteLine($"{adi} {soyadi} çalışanı bulunamadı.");
        }
    }
        static void GorevAta(List<Calisanlar> calisanlar, List<Gorev> gorevler)
    {
        Console.WriteLine("1. Mevcut görevi değiştirerek yeni bir göreve ata");
        Console.WriteLine("2. Çalışana görev ata");
        Console.Write("Seçiminizi yapın: ");
        string choice2 = Console.ReadLine();

        if (choice2 == "1")                    //Tekrardan seçim yaptırarak mevcut çalışanların mı taşınacağına yada görevi olmayan birine
        {                                      //görevmi atacğına karar veriyorum
            Console.Write("Görevden alınacak çalışan adı: ");
            string adi = Console.ReadLine();
            Console.Write("Görevden alınacak çalışan soyadı: ");
            string soyadi = Console.ReadLine();

            Calisanlar calisan = calisanlar.FirstOrDefault(e => e.Adi.Equals(adi, StringComparison.OrdinalIgnoreCase) && e.Soyadi.Equals(soyadi, StringComparison.OrdinalIgnoreCase));

            if (calisan == null || calisan.AtananGorev == 0)
            {
                Console.WriteLine("Çalışan bulunamadı veya mevcut bir görevi yok."); 
                return;                                     //Çalışanın varlığı yada atanmış biri olup olmadığını kontrol ediyorum
            }

            foreach (var gorev in gorevler)                             
            {
                if (gorev.AtananCalisan.Contains(calisan))
                {
                    gorev.AtananCalisan.Remove(calisan);                  //Görevi olan çalışan görevinden alınıyor
                    calisan.AtananGorev--;
                    Console.WriteLine($"{gorev.GorevAd} görevi {calisan.Adi} {calisan.Adi} çalışanından alındı.");
                    break;
                }
            }

            Console.Write("Yeni atanacak görev adı: ");
            string yeniGorevad = Console.ReadLine();          //Çalışan yeni görevine ekleniyor
            Gorev yeniGorev = gorevler.FirstOrDefault(t => t.GorevAd.Equals(yeniGorevad, StringComparison.OrdinalIgnoreCase));

            if (yeniGorev == null || yeniGorev.AtananCalisan.Count >= yeniGorev.GorevSinir)
            {
                Console.WriteLine("Yeni görev bulunamadı veya kapasitesi dolu.");
                return;
            }

            yeniGorev.AtananCalisan.Add(calisan);
           calisan.AtananGorev++;
            Console.WriteLine($"{yeniGorev.GorevAd} görevi {calisan.Adi} {calisan.Soyadi} çalışanına başarıyla atandı.");
        }
        else if (choice2 == "2")
        {
            Console.Write("Görev adı: ");           
            string gorevad = Console.ReadLine();
            Gorev gorev = gorevler.FirstOrDefault(t => t.GorevAd.Equals(gorevad, StringComparison.OrdinalIgnoreCase));

            if (gorev == null || gorev.AtananCalisan.Count >= gorev.GorevSinir)
            {
                Console.WriteLine("Görev bulunamadı veya kapasite dolu."); //Çalışanın varlığına ve görevde yeterli yer olup olmadığı bakılıyor
                return;
            }

            Console.Write("Çalışan adı: ");
            string adi = Console.ReadLine();
            Console.Write("Çalışan soyadı: ");
            string soyadi = Console.ReadLine();

            Calisanlar calisan = calisanlar.FirstOrDefault(e => e.Adi.Equals(adi, StringComparison.OrdinalIgnoreCase) && e.Soyadi.Equals(soyadi, StringComparison.OrdinalIgnoreCase));

            if (calisan == null || calisan.AtananGorev >= calisan.Atamasiniri)
            {
                Console.WriteLine("Çalışan bulunamadı veya başka bir göreve atanmış.");
                return;                              //Bu kısım ise daha basit bir şekilde çalışanı alıp bir göreve atıyor
            }

            gorev.AtananCalisan.Add(calisan);
            calisan.AtananGorev++;
            Console.WriteLine($"{gorev.GorevAd} görevi {calisan.Adi} {calisan.Soyadi} çalışanına atandı.");
        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");
        }
    }
    static void DurumuGoster(List<Calisanlar> calisanlar, List<Gorev> gorevler)
    {
        Console.WriteLine("\nGörev Durumları:");
        foreach (var gorev in gorevler.OrderByDescending(t => t.Onem))
        {
            string onemsirasi = gorev.Onem switch         //Önceliğine göre görevlerin yazışını sıralatıyorum
            {                                             //Her birine önceliği belirten bir sayı atanmış durumda
                1 => "Düşük",
                2 => "Orta",
                3 => "Yüksek",
                _ => "Bilinmiyor"
            };

            Console.WriteLine($"{gorev.GorevAd} (Öncelik: {onemsirasi}, Kapasite: {gorev.AtananCalisan.Count}/{gorev.GorevSinir})");
            if (gorev.AtananCalisan.Any())
            {
                Console.WriteLine("  Atanan Çalışanlar:");               //Mecut görevi olanlar görevin altında yazılıyor
                foreach (var calisan in gorev.AtananCalisan)
                {
                    Console.WriteLine($"    - {calisan.Adi} {calisan.Soyadi}");
                }
            }
            else
            {                                                       //Eğer görevde çalışan bulumuyorsa
                Console.WriteLine("  Henüz atanan yok.");
            }
        }

        Console.WriteLine("\nÇalışan Durumları:");
        foreach (var calisan in calisanlar)
        {
            Console.WriteLine($"{calisan.Adi} {calisan.Soyadi} (Atanmış Görev Sayısı: {calisan.AtananGorev}/{calisan.Atamasiniri})");
        }
    }
    static void GorevEkle(List<Gorev> gorevler)
    {
        Console.Write("Görev adı: ");
        string gorevad = Console.ReadLine();
        Console.Write("Maksimum atanacak kişi sayısı: ");
        int gorevsinir = int.Parse(Console.ReadLine());                    //Yeni eklenecek görevin adı ve sınırları alınıyor
        Console.Write("Görev önceliği (1: Düşük, 2: Orta, 3: Yüksek): ");  //Listelemede yardımcı olması için görev için bir öncelik belirleniyor
        int onem = int.Parse(Console.ReadLine());

        gorevler.Add(new Gorev { GorevAd = gorevad, GorevSinir = gorevsinir, Onem = onem });
        Console.WriteLine($"{gorevad} görevi başarıyla eklendi.");
    }
}
