using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans
{
    //static sınıfları  new 'lemey gerek yok bu sayede bellek tasarrufu vs vs.
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz.";
        public static string MaintenanceTime = "Sistem bakimda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOFCategoyError = "Daha fazla ürün eklenemez!";
        public static string ProductUpdated = "Ürün güncellendi !";
        public static string ProductNameAlreadyExist = "Bu isimden zaten bir ürün var !";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor!";
    }
}