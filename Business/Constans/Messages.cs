using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans
{
    //static sınıfları  new 'lemey gerek yok bu sayede bellek tasarrufu vs vs.
   public static  class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz.";
        public static string MaintenanceTime="Sistem bakimda";
        public static string ProductsListed="Ürünler listelendi";
    }
}
