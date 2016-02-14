using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdamAsmacaOyunuConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] isimler = { "yunanistan", "güçlü", "çekoslavakya", "tahterevalli", "ramazan" };
            Random rnd = new Random();           
            string cevap = "";
            int sonuc = -1;

            do
            {
                int sayi = rnd.Next(0, 5);
                float puan = 900;
                int hak = 6;
                string arananKelime = isimler[sayi];
                string[] tahmin = new string[arananKelime.Length]; // kullanıcı veriler girdikçe ve bildikçe tahmin dizisi değişecek.

                Console.Clear();

                for (int i = 0; i < tahmin.Length; i++)
                {
                    tahmin[i] = "-";                        // tahmin dizisinin ilk hali .. ex: ------
                }

                Console.WriteLine(string.Join(" ", tahmin));
                Console.WriteLine("harf sayısı:{0}", arananKelime.Length);
                do
                {
                    try
                    {                        
                        Console.WriteLine("\nhak:{0}  puan: {1}", hak, puan);
                        Console.WriteLine("tahmin et!");
                        Console.WriteLine("***************************************");

                        string veriGirisi = Console.ReadLine();

                        if (veriGirisi.Length > 1)   //klavyeden tek karaktere bir de aranan kelime ile aynı sayıda harf girilmesine izin verildi.
                        {
                            if (veriGirisi.Length == arananKelime.Length)
                            {
                                if (veriGirisi.ToLower() == arananKelime)
                                {
                                    sonuc = 1;

                                    break;  // kullanıcı bu durumda aranan kelimeyi doğru tahmin etmiş olur ve döngüden çıkılır.
                                }
                                else
                                {
                                    hak--;    // yanlış tahmin ederse hakkı ve puanı kırılır.
                                    puan = puan - ((puan * 15) / 100);

                                    throw new Exception("yanlış tahmin");
                                }
                            }

                            else
                                throw new Exception("tahmininizin harf sayısı yukarıdaki - işareti sayısı ile aynı olmalıdır."); 
                        }

                        char[] girilenHarfdizi = veriGirisi.ToCharArray();
                        char girilenHarf = girilenHarfdizi[0];  // tek harf girilmesi durumu için

                        if (char.IsDigit(girilenHarf))
                            throw new Exception("rakam girmeyiniz");

                        if (char.IsUpper(girilenHarf))
                            throw new Exception("küçük harf giriniz");

                        if (!char.IsLetter(girilenHarf))
                            throw new Exception("lüften harf giriniz");
                                                 
                        char[] harfler = arananKelime.ToCharArray();

                        int index = Array.IndexOf(harfler, girilenHarf);
                        int sonHarf = Array.LastIndexOf(harfler, girilenHarf);

                        if (index == -1)  //girilen tek harf ararnan kelimede mevcut değil demektir.
                        {
                            hak--;
                            puan = puan - ((puan * 15) / 100);
                        }

                        else if (sonHarf >= index) // bu durumda girilen tek harften bir veya birden çok aranan kelimede mevcuttur. 
                        {
                            for (int i = index; i <= sonHarf; i++)
                            {
                                if (harfler[i] == girilenHarf)
                                {
                                    tahmin[i] = girilenHarf.ToString(); // ekranda gözüken --- işaretleri olan yerlere bilinen harfler atanır.
                                }
                            }
                        }

                       
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                    finally
                    {
                        Console.WriteLine();
                        Console.WriteLine(string.Join(" ", tahmin));
                    }                   

                } while (hak != 0 && tahmin.Contains("-")); // hak bitmişse veya doğru tahmin değerleri girilen tahmin dizisi dolmuş ise döngüden çık.

                if (sonuc == 1 || !tahmin.Contains("-")) // doğru tahmin veya tek harf girerek doğru tahmin
                {
                    Console.WriteLine(arananKelime);
                    Console.WriteLine("tebrikler bildiniz\nPUANINIZ:{0} ",puan);
                }

                else if(hak == 0)  // oyunu bitiremeden hakkın bitmesi..
                {
                    Console.WriteLine("maalesef hakkınız bitti...");
                }

                Console.WriteLine("devam etmek ister misiniz? e/h");
                cevap = Console.ReadLine();

            } while (cevap.ToLower() =="e" );

            Environment.Exit(0);
        }
    }
}
