using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Kargo
    {
        public int Id { get; set; } // Kargo takip numarası
        public string KargoAdi { get; set; }
        public string KargoTelefon { get; set; }
        public DateTime KargoVerilisTarihi { get; set; } // Kargonun teslim alındığı tarih
        public DateTime? TahminiTeslimTarihi { get; set; } // Opsiyonel, tahmini teslim tarihi
        public EnumKargoSirketi KargoSirketi { get; set; } // Kargo şirketi seçimi için enum


        // Kargo durumu takibi için ek özellikler
        public string KargoDurumu { get; set; } // Örneğin, "Yolda", "Teslim Edildi" gibi durumlar
    }
    public enum EnumKargoSirketi
    {
        ArasKargo,
        MNGKargo,
        YurtiçiKargo,
        SüratKargo,
        PTTKargo
    }
    public enum EnumKargoDurumu
    {
        Alındı,
        Yolda,
        Dağıtımda,
        TeslimEdildi,
        TeslimEdilemedi
    }



}
