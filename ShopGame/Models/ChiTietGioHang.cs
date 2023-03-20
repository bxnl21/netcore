using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Models
{
    public class ChiTietGioHang
    {
        public int MaChiTiet { get; set; }
        public int? MaGio { get; set; }
        public int? MaHh { get; set; }
        public int? SoLuong { get; set; }
        public int? DonGia { get; set; }

        public virtual GioHang MaGioNavigation { get; set; }
        public virtual SanPham MaHhNavigation { get; set; }
    }
}
