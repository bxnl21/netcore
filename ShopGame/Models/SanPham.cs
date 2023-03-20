using System;
using System.Collections.Generic;

#nullable disable

namespace ShopGame.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int? MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int? GiaBan { get; set; }
        public int? MaLoai { get; set; }
        public string Anh { get; set; }
 
        public virtual LoaiSanPham MaLoaiNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
