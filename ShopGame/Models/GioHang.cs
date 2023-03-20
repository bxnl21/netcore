using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Models
{
    public class GioHang
    {
        public SanPham spGioHang { set; get; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return (int)(SoLuong * spGioHang.GiaBan);
            }
        }
    }
}
