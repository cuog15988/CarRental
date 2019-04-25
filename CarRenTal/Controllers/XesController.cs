using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Controllers
{
    public class XesController : Controller
    {
        private readonly RentalCarContext _context;

        public XesController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
            var rentalCarContext = _context.Xe.Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaTenXe,NgayNhap,MaNguoiDang,Tinh,Huyen")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaTenXe,NgayNhap,MaNguoiDang,Tinh,Huyen")] Xe xe)
        {
            if (id != xe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Id))
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
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xe = await _context.Xe.FindAsync(id);
            _context.Xe.Remove(xe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
            return _context.Xe.Any(e => e.Id == id);
        }
    }
}
