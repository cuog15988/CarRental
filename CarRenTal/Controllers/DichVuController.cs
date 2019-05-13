﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.wwwroot.DAO;
using CarRenTal.DAO;
using Syncfusion.EJ2.Navigations;

namespace CarRenTal.Controllers
{
    public class DichVuController : Controller
    {
        private readonly RentalCarContext _context;

        public DichVuController(RentalCarContext context)
        {
            _context = context;
        }

        public IActionResult error()
        {
            return View();
        }

        public IActionResult Index()
        {
            Link._link = Request.Headers["Referer"].ToString();
            int a = Convert.ToInt32(Seacrch.LoaiXe);
            var loaixe = _context.LoaiXe.Where(x => x.Id == a).SingleOrDefault();
            var tinh = _context.Tinh.Where(x => x.Ma == Convert.ToInt32(Seacrch.Tinh)).SingleOrDefault();
            var huyen = _context.Huyen.Where(x => x.Id == Convert.ToInt32(Seacrch.Huyen)).SingleOrDefault();
            var ren = _context.Xe.Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation).Where(x => x.TenLoai == loaixe.TenLoai && x.Tinh == tinh.TenTinh && x.Huyen == huyen.TenHuyen).ToList(); ;
            if (ren.Count >= 1)
            {
                return View(ren);
            }
            else
                return View("error");
        }
        //chi tiết xe
        public async Task<IActionResult> Detail(int? id)
        {
            string a = Link._link;
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaTenXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xe == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.headerTextOne = new TabHeader { Text = "Chi tiết xe", IconCss = "e-arrowheaddown-2x" };
                ViewBag.headerTextTwo = new TabHeader { Text = "Nhận xét", IconCss = "e-filterset" };
                ViewBag.headerTextThree = new TabHeader { Text = "Thông tin quan trọng", IconCss = "e-filterset" };
                return View(xe);
            }



            //return View(xe);
        }

        //load ds sắp xếp
        public PartialViewResult SXTenXe()
        {
            return PartialView();
        }

        public PartialViewResult SXTinh()
        {
            var tinh = _context.Tinh.Where(x => x.Ma == Convert.ToInt32(Seacrch.Tinh)).FirstOrDefault();
            return PartialView();
        }


    }
}