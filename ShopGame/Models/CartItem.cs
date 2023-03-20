using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Models
{
    public class Cartitem
    {
        public SanPham Product { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return (int)(SoLuong * Product.GiaBan);
            }
        }
    }
}
