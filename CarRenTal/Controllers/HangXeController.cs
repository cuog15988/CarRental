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
    public class HangXeController : Controller
    {
        private readonly RentalCarContext _context;

        public HangXeController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: HangXe
        public async Task<IActionResult> Index()
        {
            var rentalCarContext = _context.HangXe.Include(h => h.MaLoaiXeNavigation);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: HangXe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = await _context.HangXe
                .Include(h => h.MaLoaiXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hangXe == null)
            {
                return NotFound();
            }

            return View(hangXe);
        }

        // GET: HangXe/Create
        public IActionResult Create()
        {
            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "Id");
            return View();
        }

        // POST: HangXe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenHang,MaLoaiXe")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "Id", hangXe.MaLoaiXe);
            return View(hangXe);
        }

        // GET: HangXe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = await _context.HangXe.FindAsync(id);
            if (hangXe == null)
            {
                return NotFound();
            }
            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "Id", hangXe.MaLoaiXe);
            return View(hangXe);
        }

        // POST: HangXe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenHang,MaLoaiXe")] HangXe hangXe)
        {
            if (id != hangXe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangXeExists(hangXe.Id))
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
            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "Id", hangXe.MaLoaiXe);
            return View(hangXe);
        }

        // GET: HangXe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = await _context.HangXe
                .Include(h => h.MaLoaiXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hangXe == null)
            {
                return NotFound();
            }

            return View(hangXe);
        }

        // POST: HangXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangXe = await _context.HangXe.FindAsync(id);
            _context.HangXe.Remove(hangXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangXeExists(int id)
        {
            return _context.HangXe.Any(e => e.Id == id);
        }
    }
}
