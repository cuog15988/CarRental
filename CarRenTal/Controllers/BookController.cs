using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.DAO.Common;
using CarRenTal.wwwroot.DAO;
using CarRenTal.DAO;

namespace CarRenTal.Controllers
{
    public class BookController : BaseUserController
    {
        private readonly RentalCarContext _context;

        public BookController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return  NotFound();
        }

        // GET: Book/Details/5


        public IActionResult Xe(int id)
        {
            var donHang =  _context.DonHang
                .Include(d => d.MaLoaiThanhToanNavigation)
                .Include(d => d.MaUsNavigation)
                .Include(d => d.MaXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            return PartialView();
        }

        public PartialViewResult date()
        {
            return PartialView();
        }
        
        public IActionResult savedate(Search model)
        {

            if(ModelState.IsValid)
            {
                Seacrch.ngaynhan = model.ngaynhan;
                Seacrch.ngaytra = model.ngaytra;
                Seacrch.daydiff = (Seacrch.ngaytra - Seacrch.ngaynhan).TotalDays;
                return Redirect(Request.Headers["Referer"].ToString());
            }
            
            return NotFound();
        }



        // GET: Book/Create
        public IActionResult Create(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            CommonConstants.maxe = Convert.ToInt32(id);

            ViewData ["xe1"] = _context.Xe.Where(x => x.Id == id).ToList();

            if (CommonConstants.UserName == null)
            {
                return NotFound();
            }
            var Xes= _context.Xe.SingleOrDefault(x => x.Id == id);

            ViewData["Xe"] = _context.Xe.Include(a=>a.MaNguoiDangNavigation).SingleOrDefault(x => x.Id == id);
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id");
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MaXe"] = new SelectList(_context.Xe, "TenLoai", "TenLoai");
            ViewData["Id"] = id;
            return View();
        }
        public IActionResult getBook( int? id)
        {
            DonHang donHang = new DonHang();
            var xe = _context.Xe.SingleOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                
                donHang.MaXe = id;
                donHang.NgayLap = DateTime.Now;
                donHang.TuNgay = Seacrch.ngaynhan;
                donHang.DenNgay = Seacrch.ngaytra;
                donHang.MaUs = CommonConstants.UserID;
                donHang.Songay = Convert.ToInt32(Seacrch.daydiff);
                donHang.TongTien = Convert.ToInt32(Seacrch.daydiff * xe.Gia + 2500000);
                donHang.Status = false;
                donHang.Huy = false;

                _context.Add(donHang);
                _context.SaveChanges();

                return RedirectToAction(nameof(CustomerOder));
            }

            return NotFound();
        }

        public IActionResult CustomerOder()
        {
            return View();
        }
   

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
