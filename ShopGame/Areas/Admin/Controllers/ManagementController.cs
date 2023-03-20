using Microsoft.AspNetCore.Mvc;
using ShopGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ShopGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementController : Controller
    {
        private DbQuanLyShopGameContext context = new DbQuanLyShopGameContext();
        public ActionResult Pagination(int pg = 1)
        {
            //int checkLog = HttpContext.Session.GetInt32(ssMaNV);
            List<SanPham> lstSanPham = context.SanPhams.ToList();

            const int pageSize = 6;

            if (pg < 1)
                pg = 1;

            int recsCount = lstSanPham.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = lstSanPham.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return PartialView(data);
        }
        public ActionResult Index(int pg = 1)
        {
            List<SanPham> lstSanPham = context.SanPhams.ToList();

            const int pageSize = 6;

            if (pg < 1)
                pg = 1;

            int recsCount = lstSanPham.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = lstSanPham.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
            //    List<SanPham> sanphams = db.SanPhams.ToList();
            //    return View(sanphams);
        }
        public ActionResult Index1()
        {
            List<LoaiSanPham> lsp = context.LoaiSanPhams.ToList();
            return View(lsp);
        }
        public ActionResult AddEdit(int? idProduct)
        {
            SanPham sp = context.SanPhams.FirstOrDefault(x => x.MaSanPham == idProduct);
            ViewBag.ListCategory = context.LoaiSanPhams.ToList();
            if (sp != null)
                return View(sp);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddOrEditAsync(SanPham sp)
        {
            string fName = null;
            var filePath = Path.GetTempFileName();
            foreach (var formFile in Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    using (var inputStream = new FileStream(filePath, FileMode.Create))
                    {
                        // read file to stream
                        await formFile.CopyToAsync(inputStream);
                        // stream to byte array
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                        // get file name
                        fName = formFile.FileName;
                    }
                }
            }

            if (sp.MaSanPham == null)
            {
                if (ModelState.IsValid)
                {
                    SanPham spThem = new SanPham();

                    spThem.TenSanPham = sp.TenSanPham;
                    spThem.Anh = fName;
                    spThem.GiaBan = sp.GiaBan;
                    spThem.MaLoai = sp.MaLoai;

                    context.SanPhams.Add(spThem);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                    //Them
                }
                return View("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SanPham spSua = context.SanPhams.FirstOrDefault(x => x.MaSanPham == sp.MaSanPham);

                    spSua.TenSanPham = sp.TenSanPham;

                    spSua.GiaBan = sp.GiaBan;
                    spSua.MaLoai = sp.MaLoai;
                    spSua.Anh = fName;

                    context.SaveChanges();
                    //Sua

                    return RedirectToAction("Index");
                }
                return View("Index");
            }
        }

        public ActionResult Delete(int idPro)
        {
            SanPham spDelete = context.SanPhams.FirstOrDefault(x => x.MaSanPham == idPro);
            context.SanPhams.Remove(spDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
