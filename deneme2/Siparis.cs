﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisOtomasyonu
{
   public   class Siparis
    {
        public  DateTime SiparisTarihi { get; set; }
        public  musteri Musteri { get; set; }
        public  string SiparisDurumu { get; set; }
        public  List<SiparisDetay> Detaylar { get; set; }//aggregation ilişki
        public Siparis(musteri m)
        {   
            this.Musteri = m;
            
            //this.Detaylar = new List<SiparisDetay>();//composition ilişki
        }
        public void SiparisDetayEkle(SiparisDetay detay)
        {
            Detaylar.Add(detay);
        }
        public string SiparisListele()
        {
            string Metin = "";
            foreach (SiparisDetay item in Detaylar)
            {
                Metin +=  Musteri.MusteriID.ToString() + " " + Musteri.AdSoyad + " " + item.Uruntanimi.UrunAdi + " " + item.Uruntanimi.UrunSecilenMiktar + " " + item.Uruntanimi.UrunFiyat + Environment.NewLine;
            }
            return Metin;
        }
        public double ToplamTutarHesapla()
        {
            double toplamtutar = 0;
            foreach (SiparisDetay item in Detaylar)
            {
                toplamtutar += item.Aratoplam()+(item.CalcWeight()*0.05);//ürün agırlına göre kargo parası eklendi birim başı 0.5
            }
            return toplamtutar;
        }
        public double ToplamKargoUcreti()
        {
            double toplamtutar = 0;
            foreach (SiparisDetay item in Detaylar)
            {
                toplamtutar += (item.CalcWeight() * 0.05);//ürün agırlına göre kargo parası eklendi birim başı 0.5
            }
            return toplamtutar;
        }


        public double CalcTotalWeight()//toplam ürün ağırlıgı
        {
            double toplamAgırlık = 0;
            foreach (SiparisDetay item in Detaylar)
            {
                
                toplamAgırlık += item.Uruntanimi.GetWeight();
            }
            return toplamAgırlık;
        }
        public double CalcTax()
        {
            double toplamVergiTutari =0 ;
            foreach (SiparisDetay item in Detaylar)
            {
                toplamVergiTutari += item.Vergi;
            }
            return toplamVergiTutari;
        }
    }
}

