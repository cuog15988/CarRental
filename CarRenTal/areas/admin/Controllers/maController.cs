using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Areas.admin.Controllers
{
    [Area("admin")]
    public class maController : Controller
    {
        private readonly RentalCarContext _context;

        public maController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: admin/ma
        public async Task<IActionResult> Index()
        {
            var rentalCarContext = _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: admin/ma/Details/5
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

        // GET: admin/ma/Create
        public IActionResult Create()
        {
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id");
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id");
            return View();
        }

        // POST: admin/ma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
            return View(donHang);
        }

        // GET: admin/ma/Edit/5
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

        // POST: admin/ma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status")] DonHang donHang)
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

        // GET: admin/ma/Delete/5
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

        // POST: admin/ma/Delete/5
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
