using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopGame.Models;
using ShopGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Controllers
{
    public class HomeController : Controller
    {
        DbQuanLyShopGameContext db = new DbQuanLyShopGameContext();
        private readonly ILogger<HomeController> _logger;
        private readonly DbQuanLyShopGameContext _context;

        public HomeController(ILogger<HomeController> logger, DbQuanLyShopGameContext context)
        {
            _logger = logger;
            _context = db;
        }

        public IActionResult Index()
        {
            DbQuanLyShopGameContext context = new DbQuanLyShopGameContext();
            List<SanPham> lstSanPam = context.SanPhams.ToList();
            return View(lstSanPam);
        }
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login", new { area = "Admin" });
        }
        public ActionResult ShowProduct(int maSanPham)
        {
            List<SanPham> lstSP = null;
            using (DbQuanLyShopGameContext context = new DbQuanLyShopGameContext())
            {
                lstSP = context.SanPhams.Join(context.LoaiSanPhams,
                    s => s.MaLoai,
                    l => l.MaLoai,
                    (s, l) => new SanPham
                    {
                        MaLoai = s.MaLoai,
                        MaSanPham = s.MaSanPham,
                        GiaBan = s.GiaBan,
                        TenSanPham = s.TenSanPham,
                        Anh = s.Anh
                    }).Where(x => x.MaLoai == maSanPham).ToList();
                return PartialView(lstSP);
            }
        }
        public ActionResult Detail(int id)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSanPham == id);
            return View("Detail", sp);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public SanPham getDetailProduct(int id)
        {
            var spGioHang = db.SanPhams.Find(id);
            return spGioHang;
        }
        public IActionResult addCart(int id, int soluong)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null && soluong != 0)
            {
                var spGioHang = getDetailProduct((int)id);
                List<GioHang> listCart = new List<GioHang>()
                {
                    new GioHang
                    {
                        spGioHang = spGioHang,
                        SoLuong = soluong



                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));

            }
            else if (cart == null && soluong == 0)
            {
                var spGioHang = getDetailProduct((int)id);
                List<GioHang> listCart = new List<GioHang>()
                {
                    new GioHang
                    {
                        spGioHang = spGioHang,
                        SoLuong = 1



                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                bool check = true;
                if (soluong == 0)
                {
                    soluong = 1;
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].spGioHang.MaSanPham == id)
                        {
                            dataCart[i].SoLuong += soluong;
                            check = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].spGioHang.MaSanPham == id)
                        {
                            dataCart[i].SoLuong += soluong;
                            check = false;
                        }
                    }
                }
                if (check)
                {
                    var spGioHang = getDetailProduct((int)id);
                    dataCart.Add(new GioHang
                    {
                        spGioHang = getDetailProduct((int)id),
                        SoLuong = 1
                    });
                }

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult updateCart(int id, int soluong)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                if (soluong > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].spGioHang.MaSanPham == id)
                        {
                            dataCart[i].SoLuong = soluong;
                        }
                    }
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(soluong);
            }
            return BadRequest();
        }
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].spGioHang.MaSanPham == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult ShopLoai(int ma, int pageNumber = 1)
        {
            DbQuanLyShopGameContext context = new DbQuanLyShopGameContext();
            const int pageSize = 12;

            List<SanPham> dsHH = context.SanPhams.Where(x => x.MaLoai == ma).ToList();
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int recsCount = dsHH.Count;

            var pager = new Pager(recsCount, pageNumber, pageSize);

            int recSkip = (pageNumber - 1) * pageSize;

            var data = dsHH.Skip(recSkip).Take(pager.TotalPages).ToList();

            this.ViewBag.Pager = pager;

            return PartialView(data);
        }
        public ActionResult XuatTheoMa(int? ma)
        {
            List<SanPhamVM> lstHH = new List<SanPhamVM>();
            using (var context = new DbQuanLyShopGameContext())
            {
                lstHH = context.SanPhams.Join(context.LoaiSanPhams,
                    p => p.MaLoai,
                    c => c.MaLoai,
                    (p, c) =>
                    new SanPhamVM
                    {
                        MaSP = p.MaSanPham,
                        TenSP = p.TenSanPham,
                        MaLoai = p.MaLoai,
                        TenLoai = c.TenLoai,
                        GiaBan = p.GiaBan,
                        Hinh = p.Anh,
                    }).Where(x => x.MaSP == ma).ToList();
                return PartialView(lstHH);
            }
        }
    }
}
