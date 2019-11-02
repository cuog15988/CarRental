using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.wwwroot.DAO;
using CarRenTal.DAO;
using Syncfusion.EJ2.Navigations;
using CarRenTal.DAO.Common;

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

        public IActionResult saveCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var xe = _context.Xe.SingleOrDefault(x => x.Id == id);
            Cart c = new Cart();
            if (xe == null)
            {
                return NotFound();
            }

            var cart = _context.Cart.SingleOrDefault(x => x.Maxe == id && x.Ma == CommonConstants.UserID);
            if (cart == null)
            {

                if (ModelState.IsValid)
                {
                    c.Ma = CommonConstants.UserID;
                    c.Manguoidang = xe.MaNguoiDang;
                    c.Maxe = xe.Id;
                    c.Gia = xe.Gia;
                    c.Tenxe = xe.Tenxe;
                    c.DiaChi = xe.Diachi;
                    c.Images = xe.Hinh;

                }
                _context.Cart.Add(c);
                _context.SaveChanges();
                
            }
            else
            {
                ViewData["Loi"] = "Đã lưu xe trước đó rồi";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            string referer1 = Request.Headers["Referer"].ToString();
            return Redirect(referer1);
        }

        public IActionResult thaydoihuyen(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                Seacrch.Huyen = id.ToString();
                
            }
            string referer1 = Request.Headers["Referer"].ToString();

            return Redirect(referer1);

        }

        public IActionResult Index(int? mahang)
        {
            try
            {

            ViewData["tinh"] = _context.Xe.Include(x => x.MaHuyenNavigation).Include(x => x.MaHuyenNavigation.MaTinhNavigation)
                .Where(x => x.MaHuyenNavigation.MaTinh == Convert.ToInt32(Seacrch.Tinh)).OrderByDescending(x=>x.MaHuyen).ToList();

            ViewData["TenHang"] =_context.Xe.Include(x=>x.MaHangXeNavigation)
                    .Include(x=>x.MaHangXeNavigation.MaLoaiXeNavigation)
                    .Where(x => x.MaHuyenNavigation.MaTinh == Convert.ToInt32(Seacrch.Tinh)).OrderByDescending(x => x.MaHangXe).ToList();

                Link._link = Request.Headers["Referer"].ToString();
            int a = Convert.ToInt32(Seacrch.LoaiXe);
            var loaixe = _context.LoaiXe.Where(x => x.Id == a).SingleOrDefault();
         
                if(mahang==null)
                {
                    var ren = _context.Xe.Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHuyenNavigation)
                .Include(x => x.MaHangXeNavigation)
                .Where(x => x.TenLoai == loaixe.TenLoai && x.MaHuyen == Convert.ToInt32(Seacrch.Huyen)).ToList();
                    if (ren.Count >= 1)
                    {
                        return View(ren);
                    }
                    else
                        return View("error");
                }
                else
                {
                    Seacrch.mahang = Convert.ToInt32(mahang);
                    var ren = _context.Xe.Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHuyenNavigation)
                .Include(x => x.MaHangXeNavigation)
                .Where(x => x.TenLoai == loaixe.TenLoai && x.MaHuyen == Convert.ToInt32(Seacrch.Huyen) && x.MaHangXe==mahang).ToList();
                    if (ren.Count >= 1)
                    {
                        return View(ren);
                    }
                    else
                        return View("error");
                }
           
            }
            catch
            {
                return View("error");
            }
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
                .Include(x => x.MaHangXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.Images = _context.Images.Where(x => x.XeId == id).ToList();
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
            return PartialView();
        }


    }
}