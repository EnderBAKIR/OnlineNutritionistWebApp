using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Order : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        // Siparişin içindeki ürünler
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        // Toplam tutar

        public decimal TotalAmount { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int PackageIdentity { get; set; }
        public decimal Price { get; set; }
    }
}
