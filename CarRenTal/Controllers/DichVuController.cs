using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Migrations;
using CarRenTal.wwwroot.DAO;

namespace CarRenTal.Controllers
{
    public class DichVuController : Controller
    {
        private readonly RentalCarContext _context;

        public DichVuController(RentalCarContext context)
        {
            _context = context;
        }

        public IActionResult error ()
        {
            return View();
        }

        public IActionResult Index()
        {
            int a = Convert.ToInt32(Seacrch.LoaiXe);
            var loaixe = _context.LoaiXe.Where(x => x.Id == a).SingleOrDefault();
            var tinh = _context.Tinh.Where(x => x.Ma == Convert.ToInt32(Seacrch.Tinh)).SingleOrDefault();
            var huyen = _context.Huyen.Where(x => x.Id == Convert.ToInt32(Seacrch.Huyen)).SingleOrDefault();
            var ren = _context.Xe.Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation).Where(x => x.TenLoai == loaixe.TenLoai && x.Tinh == tinh.TenTinh && x.Huyen == huyen.TenHuyen).ToList(); ;
            if (ren.Count>=1)
            {
                return View(ren);
            }
            else
                return View("error");
        }

        public PartialViewResult SXTenXe()
        {
            var result = _context.HangXe.Where(x => x.MaLoaiXe == Convert.ToInt32(Seacrch.LoaiXe)).ToList();
            return PartialView(result);
        }

        public PartialViewResult SXTinh()
        {
            var result = _context.Huyen.Where(x => x.MaTinh == Convert.ToInt32(Seacrch.Tinh)).ToList();
            return PartialView(result);
        }
    }
}