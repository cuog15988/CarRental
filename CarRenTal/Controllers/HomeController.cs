using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using System.Diagnostics;
using CarRenTal.wwwroot.DAO;
using CarRenTal.DAO.Common;
using CarRenTal.DAO;

namespace CarRenTal.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentalCarContext _context;

        public HomeController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "TenLoai");
            ViewBag.Tinh = _context.Tinh.ToList();
            return View();
        }
       
        public JsonResult getHuyenbyID (int id)
        {
            var citylist = new SelectList(_context.Huyen.Where(c => c.MaTinh == id), "Id", "TenHuyen");
            return Json(citylist);

        }

        public JsonResult getHangXe(int id)
        {
            var citylist1 = new SelectList(_context.HangXe.Where(c => c.MaLoaiXe == id), "Id", "TenHang");
            return Json(citylist1);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Search hangXe)
        {
            List<DateTime> AuthorList = new List<DateTime>();
            
            if (ModelState.IsValid)
            {
                Seacrch.Huyen = hangXe.Huyen;
                Seacrch.Tinh = hangXe.Tinh;
                Seacrch.LoaiXe = hangXe.TenLoai;
                Seacrch.ngaytra = hangXe.ngaytra;
                Seacrch.ngaynhan = hangXe.ngaynhan;
                if (string.IsNullOrEmpty(hangXe.TenLoai))
                {
                    ViewData["Loi1"] = "Không được để trống mục này";
                }
                else
                  if (string.IsNullOrEmpty(hangXe.Tinh))
                {
                    ViewData["loi2"] = "Không được để trống mục này";
                }
                else
                     if (string.IsNullOrEmpty(hangXe.Huyen))
                {
                    ViewData["Loi3"] = "Không được để trống mục này";
                }
                else
                {
                    Seacrch.daydiff = (hangXe.ngaytra - hangXe.ngaynhan).TotalDays;
                    return RedirectToAction("index", "DichVu");
                }
            }
            return this.View();
        }



        public JsonResult GetStateList(int CountryId)
        {
            List<Huyen> StateList = _context.Huyen.Where(x => x.MaTinh == CountryId).ToList();
            return Json(StateList);

        }

        public JsonResult getOder(int id)
        {
            List<DonHang> StateList = _context.DonHang.Include(x=>x.MaXeNavigation).Where(x => x.MaUs == id).ToList();
            return Json(StateList);
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


    }
}
