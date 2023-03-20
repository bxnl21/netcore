using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private DbQuanLyShopGameContext context = new DbQuanLyShopGameContext();
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Check(NhanVien nv)
        {
            NhanVien nvCheck = context.NhanViens.FirstOrDefault(x => x.Id == nv.Id && x.PassWordd == nv.PassWordd);
            if (nvCheck != null)
            {
                string ssMaNV = "zzz";
                HttpContext.Session.SetInt32(ssMaNV, nvCheck.MaNhanVien);
                int test = (int)HttpContext.Session.GetInt32(ssMaNV);
                return RedirectToAction("Index", "Management", null);
            }
            else
                return View("Index");
        }

    }
}
