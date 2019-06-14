using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.DAO.Common;
using CarRenTal.Models;
using CarRenTal.Controllers;

namespace CarRenTal.Areas.User.Controllers
{
    [Area("User")]
    public class CustomerController : BaseUserController
    {
        private readonly RentalCarContext _context;

        public CustomerController(RentalCarContext context)
        {
            _context = context;
        }

        public IActionResult huyyeucau(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                var s = _context.DonHang.Find(id);
                s.Huy = true;
                _context.DonHang.Update(s);
                _context.SaveChanges();
                string referer1 = Request.Headers["Referer"].ToString();
                return Redirect(referer1);
            }
            return Redirect(Request.Headers["Referer"].ToString());

        }
        // GET: User/Customer
        public async Task<IActionResult> oders(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            if(id != CommonConstants.UserID)
            {
                return NotFound();
            }
            try
            {
                var rentalCarContext = _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation).Include(e => e.MaXeNavigation.MaHuyenNavigation)
                    .Include(e=>e.MaXeNavigation.MaHangXeNavigation).Include(f => f.MaXeNavigation.MaNguoiDangNavigation).Where(x => x.MaUs == id).OrderByDescending(x=>x.NgayLap);
                return View(await rentalCarContext.ToListAsync());

            }
            catch {
                return NotFound();
            }
        }

        // GET: User/Customer/Details/5
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
                .Include(e => e.MaXeNavigation.MaNguoiDangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: User/Customer/Create
        //public IActionResult Create()
        //{
        //    ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id");
        //    ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id");
        //    ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id");
        //    return View();
        //}

        // POST: User/Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status")] DonHang donHang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(donHang);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(oders));
        //    }
        //    ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
        //    ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
        //    ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
        //    return View(donHang);
        //}

        // GET: User/Customer/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var donHang = await _context.DonHang.FindAsync(id);
        //    if (donHang == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
        //    ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
        //    ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
        //    return View(donHang);
        //}

        // POST: User/Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status")] DonHang donHang)
        //{
        //    if (id != donHang.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(donHang);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DonHangExists(donHang.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(oders));
        //    }
        //    ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
        //    ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
        //    ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
        //    return View(donHang);
        //}

        // GET: User/Customer/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var donHang = await _context.DonHang
        //        .Include(d => d.MaLoaiThanhToanNavigation)
        //        .Include(d => d.MaUsNavigation)
        //        .Include(d => d.MaXeNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (donHang == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(donHang);
        //}

        // POST: User/Customer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var donHang = await _context.DonHang.FindAsync(id);
        //    _context.DonHang.Remove(donHang);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(oders));
        //}

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
