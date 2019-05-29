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
            var rentalCarContext = _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.MaLoaiThanhToanNavigation)
                .Include(d => d.MaUsNavigation)
                .Include(d => d.MaXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }


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

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        //public  IActionResult Create(DonHang donHang, int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        donHang.MaXe = id;
        //        donHang.NgayLap = DateTime.Now;
        //        donHang.TuNgay = Seacrch.ngaynhan;
        //        donHang.DenNgay = Seacrch.ngaytra;
        //        donHang.MaUs = CommonConstants.UserID;
        //        _context.Add(donHang);
        //        _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
        //    ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
        //    ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
        //    return View(donHang);
        //}

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
            return View(donHang);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status,Songay,TongTien,Giamgia")] DonHang donHang)
        {
            if (id != donHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
            return View(donHang);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.MaLoaiThanhToanNavigation)
                .Include(d => d.MaUsNavigation)
                .Include(d => d.MaXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHang.FindAsync(id);
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
